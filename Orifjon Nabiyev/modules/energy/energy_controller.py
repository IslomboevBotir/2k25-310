"""
modules/energy/energy_controller.py

EnergyController and MeterGroup (Composite) for the energy subsystem.

Patterns used:
- Composite: MeterGroup composes MeterComponent instances (Meter or decorated meters),
  allowing aggregated operations and uniform treatment of individuals and groups.
- Facade: EnergyController provides a simplified interface (monitoring lifecycle, totals, energy-saving hints)
  to orchestrate meters and present a concise API to the CityController.

This controller is written to integrate cleanly with other subsystems (lighting, transport) while remaining
self-contained for unit testing and demos.
"""

from __future__ import annotations
from typing import Dict, Any, List, Optional
from meter import MeterComponent
import threading
import time


class MeterGroup(MeterComponent):
    """
    Composite that aggregates MeterComponent children.
    """

    def __init__(self, name: str):
        self.name = name
        self._members: List[MeterComponent] = []

    def add(self, comp: MeterComponent) -> None:
        self._members.append(comp)

    def remove(self, comp: MeterComponent) -> None:
        self._members = [m for m in self._members if m is not comp]

    def read_kwh(self) -> float:
        total = 0.0
        for m in self._members:
            try:
                total += float(m.read_kwh())
            except Exception:
                continue
        return round(total, 4)

    def record_usage(self, kwh: float) -> None:
        """
        Distribute incoming kWh evenly across members as a simple simulation of aggregated metering.
        In real systems distribution would be per-meter.
        """
        if not self._members:
            return
        per = float(kwh) / len(self._members)
        for m in self._members:
            try:
                m.record_usage(per)
            except Exception:
                continue

    def report(self) -> Dict[str, Any]:
        return {
            "group": self.name,
            "kwh_total": self.read_kwh(),
            "members": [m.report() for m in self._members],
            "count": len(self._members),
        }

    def status(self) -> Dict[str, Any]:
        return {
            "group": self.name,
            "count": len(self._members),
            "members": [m.status() for m in self._members],
        }

    def get_members(self) -> List[MeterComponent]:
        return list(self._members)


class EnergyController:
    """
    Facade for the energy subsystem.

    Responsibilities:
      - Manage meters and meter groups.
      - Provide monitoring lifecycle methods (start_monitoring/stop_monitoring).
      - Compute aggregated metrics and offer simple energy-saving hints (e.g., top consumers).
      - Provide status and reports consumable by CityController.
    """

    def __init__(self, name: str = "EnergyController", sample_interval_secs: int = 10):
        self.name = name
        self._top_level_groups: Dict[str, MeterGroup] = {}
        self._lock = threading.RLock()
        self._running = False
        self._sample_interval = int(sample_interval_secs)
        self._thread: Optional[threading.Thread] = None
        self._stop_event = threading.Event()

    def add_group(self, group: MeterGroup) -> None:
        with self._lock:
            self._top_level_groups[group.name] = group
            print(f"[EnergyController:{self.name}] Added meter group '{group.name}'.")

    def remove_group(self, name: str) -> None:
        with self._lock:
            if name in self._top_level_groups:
                del self._top_level_groups[name]
                print(f"[EnergyController:{self.name}] Removed meter group '{name}'.")

    def start_monitoring(self) -> None:
        with self._lock:
            if self._running:
                print(f"[EnergyController:{self.name}] Monitoring already running.")
                return
            self._running = True
            self._stop_event.clear()
            self._thread = threading.Thread(target=self._monitor_loop, daemon=True)
            self._thread.start()
            print(f"[EnergyController:{self.name}] Monitoring started (interval={self._sample_interval}s).")

    def stop_monitoring(self) -> None:
        with self._lock:
            if not self._running:
                print(f"[EnergyController:{self.name}] Monitoring not running.")
                return
            self._stop_event.set()
            if self._thread:
                self._thread.join(timeout=1.0)
            self._running = False
            print(f"[EnergyController:{self.name}] Monitoring stopped.")

    def _monitor_loop(self) -> None:
        """Background sampling loop that prints lightweight summaries for demo purposes."""
        while not self._stop_event.is_set():
            with self._lock:
                summary = self.get_aggregate_report()
                print(f"[EnergyController:{self.name}] Sampled at {time.strftime('%H:%M:%S')}: total_kwh={summary.get('total_kwh'):.4f}, top_consumers={summary.get('top_consumers')}")
            self._stop_event.wait(self._sample_interval)

    def get_aggregate_report(self) -> Dict[str, Any]:
        """
        Compute total kWh across all groups and identify top N groups by consumption.
        """
        with self._lock:
            groups = []
            for g in self._top_level_groups.values():
                try:
                    kwh = float(g.read_kwh())
                except Exception:
                    kwh = 0.0
                groups.append((g.name, kwh))
            total_kwh = round(sum(k for _, k in groups), 4)
            # sort descending
            groups.sort(key=lambda x: x[1], reverse=True)
            top = [{"group": name, "kwh": k} for name, k in groups[:3]]
            return {"total_kwh": total_kwh, "top_consumers": top, "groups": [{ "name": name, "kwh": k } for name, k in groups]}

    def energy_saving_hints(self, threshold_kw: float = 5.0) -> List[Dict[str, Any]]:
        """
        Provide simple hints: groups whose recent average power (approx) exceeds threshold.
        Since Meter(s) may not expose instantaneous power, approximation uses delta over sample interval if possible.
        """
        hints = []
        with self._lock:
            for g in self._top_level_groups.values():
                try:
                    kwh = g.read_kwh()
                except Exception:
                    kwh = 0.0
                # naive heuristic: if group's kWh over last sample interval exceeds threshold (kW * hours)
                # threshold_kw (kW) * (sample_interval / 3600) gives kWh threshold for interval.
                kwh_threshold = threshold_kw * (self._sample_interval / 3600.0)
                if kwh > kwh_threshold:
                    hints.append({"group": g.name, "kwh": kwh, "advice": "Investigate high consumption; consider demand response."})
        return hints

    def report(self) -> Dict[str, Any]:
        with self._lock:
            return {
                "name": self.name,
                "running": self._running,
                "aggregate": self.get_aggregate_report(),
            }

    def status(self) -> Dict[str, Any]:
        with self._lock:
            return {
                "name": self.name,
                "running": self._running,
                "groups": {name: g.status() for name, g in self._top_level_groups.items()},
            }


# Demo when run directly
if __name__ == "__main__":
    from meter import MeterTypeFlyweightFactory, Meter, ConsumptionDecorator

    print("Demo: EnergyController with MeterGroup and meters")
    factory = MeterTypeFlyweightFactory
    mt = factory.get("EnerCo", "SmartX-1000", "A")

    m1 = Meter(location="Block-1", meter_type=mt, identifier="E-M-01")
    m2 = Meter(location="Block-2", meter_type=mt, identifier="E-M-02")
    # wrap m2 with analytics
    m2_analytics = ConsumptionDecorator(m2, window_seconds=60, alert_kw_threshold=2.0)

    group = MeterGroup("Residential-Cluster")
    group.add(m1)
    group.add(m2_analytics)

    controller = EnergyController(sample_interval_secs=3)
    controller.add_group(group)

    controller.start_monitoring()
    # simulate some usage events
    for _ in range(4):
        group.record_usage(1.2)  # distribute 1.2 kWh across members
        time.sleep(1)
    time.sleep(4)
    controller.stop_monitoring()

    print("Final report:", controller.report())
    print("Status:", controller.status())