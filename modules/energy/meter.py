"""
modules/energy/meter.py

Meter implementations for the energy subsystem.

Patterns used:
- Flyweight: MeterTypeFlyweightFactory provides shared intrinsic attributes for meter types
  (e.g. manufacturer, accuracy class) so many Meter instances can reuse the same intrinsic data.
- Decorator: ConsumptionDecorator wraps a MeterComponent to add analytics like rolling-average
  consumption or threshold alerts without changing the Meter implementation.
- Composite (via MeterGroup in energy_controller) interoperates with MeterComponent so groups
  and individual meters are treated uniformly.

This file provides:
- MeterComponent (abstract interface)
- Meter (concrete smart meter)
- MeterTypeFlyweightFactory (flyweight factory for meter intrinsic properties)
- ConsumptionDecorator (decorator adding analytics)
"""

from __future__ import annotations
from abc import ABC, abstractmethod
from typing import Dict, Any, Tuple
import threading
import time
import uuid


class MeterComponent(ABC):
    """Abstract component interface for meters and composites."""

    @abstractmethod
    def read_kwh(self) -> float:
        """Return cumulative kWh recorded by this meter (or aggregated group)."""
        pass

    @abstractmethod
    def report(self) -> Dict[str, Any]:
        """Return a small report dict describing current readings and health."""
        pass

    @abstractmethod
    def record_usage(self, kwh: float) -> None:
        """Simulate recording additional consumption (kWh)."""
        pass

    @abstractmethod
    def status(self) -> Dict[str, Any]:
        """Return diagnostics/status, used by controllers."""
        pass


class MeterTypeFlyweight:
    """
    Flyweight holding intrinsic, sharable attributes of a meter type.
    Small immutable structure describing manufacturer and accuracy.
    """
    __slots__ = ("manufacturer", "model", "accuracy_class")

    def __init__(self, manufacturer: str, model: str, accuracy_class: str = "A"):
        self.manufacturer = manufacturer
        self.model = model
        self.accuracy_class = accuracy_class

    def describe(self) -> Dict[str, str]:
        return {
            "manufacturer": self.manufacturer,
            "model": self.model,
            "accuracy_class": self.accuracy_class,
        }


class MeterTypeFlyweightFactory:
    """
    Factory that caches MeterTypeFlyweight instances keyed by their descriptors.
    Ensures many Meter objects can share identical intrinsic type metadata.
    """
    _lock = threading.Lock()
    _cache: Dict[Tuple[str, str, str], MeterTypeFlyweight] = {}

    @classmethod
    def get(cls, manufacturer: str, model: str, accuracy_class: str = "A") -> MeterTypeFlyweight:
        key = (manufacturer, model, accuracy_class)
        with cls._lock:
            if key not in cls._cache:
                cls._cache[key] = MeterTypeFlyweight(manufacturer, model, accuracy_class)
            return cls._cache[key]

    @classmethod
    def available_types(cls) -> Dict[str, Dict[str, str]]:
        return {str(k): v.describe() for k, v in cls._cache.items()}


class Meter(MeterComponent):
    """
    Concrete Meter (leaf) representing a physical energy meter.

    Extrinsic state (unique per meter): identifier, location, cumulative_kwh, last_update_ts
    Intrinsic state (shared): meter type flyweight with manufacturer/model/accuracy
    """

    def __init__(self, location: str, meter_type: MeterTypeFlyweight, identifier: str | None = None):
        self.identifier = identifier or f"meter-{uuid.uuid4().hex[:8]}"
        self.location = location
        self._type = meter_type
        self._cumulative_kwh = 0.0
        self._last_update_ts = None
        self._operational = True
        self._lock = threading.RLock()

    def read_kwh(self) -> float:
        with self._lock:
            return round(self._cumulative_kwh, 4)

    def record_usage(self, kwh: float) -> None:
        """Add kWh to cumulative total (simulates a reading update)."""
        if kwh < 0:
            raise ValueError("kwh must be non-negative")
        with self._lock:
            self._cumulative_kwh += float(kwh)
            self._last_update_ts = time.time()
        # lightweight logging for demo
        print(f"[Meter:{self.identifier}] Recorded {kwh:.4f} kWh (total={self._cumulative_kwh:.4f}).")

    def report(self) -> Dict[str, Any]:
        with self._lock:
            return {
                "id": self.identifier,
                "location": self.location,
                "kwh": round(self._cumulative_kwh, 4),
                "last_update_ts": self._last_update_ts,
                "type": self._type.describe(),
                "operational": self._operational,
            }

    def status(self) -> Dict[str, Any]:
        with self._lock:
            uptime = None
            if self._last_update_ts:
                uptime = round(time.time() - self._last_update_ts, 1)
            return {
                "id": self.identifier,
                "location": self.location,
                "operational": self._operational,
                "last_update_seconds_ago": uptime,
                "kwh": round(self._cumulative_kwh, 4),
            }


class ConsumptionDecorator(MeterComponent):
    """
    Decorator that wraps a MeterComponent and adds simple analytics:
      - rolling window average power (kW) computed from recent updates
      - threshold alerts when usage rate exceeds configured limit
    Demonstrates Decorator pattern usage to extend behavior.
    """

    def __init__(self, wrapped: MeterComponent, window_seconds: int = 60, alert_kw_threshold: float | None = None):
        self._wrapped = wrapped
        self._window_seconds = int(window_seconds)
        self._alert_kw_threshold = float(alert_kw_threshold) if alert_kw_threshold is not None else None
        # store tuples of (timestamp, cumulative_kwh_snapshot) to compute rates
        self._snapshots: list[tuple[float, float]] = []
        self._lock = threading.RLock()

    def _snapshot(self):
        """Take a timestamped cumulative reading for rate calculations."""
        ts = time.time()
        kwh = float(self._wrapped.read_kwh())
        with self._lock:
            self._snapshots.append((ts, kwh))
            # prune older than window
            cutoff = ts - self._window_seconds
            self._snapshots = [(t, v) for (t, v) in self._snapshots if t >= cutoff]

    def record_usage(self, kwh: float) -> None:
        # Forward to wrapped meter, then snapshot for analytics
        self._wrapped.record_usage(kwh)
        self._snapshot()

        # After snapshot, evaluate alert threshold if configured
        if self._alert_kw_threshold is not None:
            avg_kw = self._compute_average_kw()
            if avg_kw is not None and avg_kw > self._alert_kw_threshold:
                print(f"[ConsumptionDecorator:{getattr(self._wrapped, 'identifier', 'unknown')}] ALERT: avg power {avg_kw:.3f} kW > threshold {self._alert_kw_threshold} kW")

    def read_kwh(self) -> float:
        return self._wrapped.read_kwh()

    def _compute_average_kw(self) -> float | None:
        """
        Compute average kW over the available snapshots in the window.
        kW = delta_kwh / delta_hours
        """
        with self._lock:
            if len(self._snapshots) < 2:
                return None
            # oldest and newest
            t0, k0 = self._snapshots[0]
            t1, k1 = self._snapshots[-1]
            delta_kwh = k1 - k0
            delta_hours = max(1e-6, (t1 - t0) / 3600.0)
            avg_kw = delta_kwh / delta_hours
            return round(avg_kw, 4)

    def report(self) -> Dict[str, Any]:
        base = self._wrapped.report()
        avg_kw = self._compute_average_kw()
        base["analytics"] = {
            "window_seconds": self._window_seconds,
            "avg_kw": avg_kw,
            "alert_threshold_kw": self._alert_kw_threshold,
        }
        return base

    def status(self) -> Dict[str, Any]:
        s = self._wrapped.status()
        avg_kw = self._compute_average_kw()
        s.setdefault("decorators", [])
        s["decorators"].append(self.__class__.__name__)
        s["analytics"] = {"avg_kw": avg_kw, "window_seconds": self._window_seconds}
        return s


# Demo when run directly
if __name__ == "__main__":
    print("Demo: Meter + Flyweight + ConsumptionDecorator")
    factory = MeterTypeFlyweightFactory
    t1 = factory.get("EnerCo", "SmartX-1000", "A")
    t2 = factory.get("LegacyInc", "Analog-50", "C")

    m1 = Meter(location="Building-A", meter_type=t1, identifier="M-A-01")
    m2 = Meter(location="Building-B", meter_type=t1, identifier="M-B-01")
    m3 = Meter(location="Old-House", meter_type=t2, identifier="M-OLD-1")

    # wrap m1 with analytics decorator
    m1_analytics = ConsumptionDecorator(m1, window_seconds=120, alert_kw_threshold=10.0)

    m1_analytics.record_usage(0.5)
    time.sleep(0.5)
    m1_analytics.record_usage(0.7)
    time.sleep(0.5)
    m1_analytics.record_usage(0.3)

    print("m1 report:", m1_analytics.report())
    print("m2 status:", m2.status())
    print("Meter types cached:", MeterTypeFlyweightFactory.available_types())