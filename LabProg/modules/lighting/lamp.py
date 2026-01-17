"""
modules/lighting/lamp.py

Lamp implementations for the lighting subsystem.

Patterns used:
- Flyweight: LampTypeFlyweight and LampTypeFlyweightFactory share intrinsic state of lamp types
  (bulb characteristics) to reduce memory when many similar lamps exist.
- Composite (leaf): Lamp implements a LightingComponent interface so it can be used inside Zone (composite).
- Decorator: DimmingDecorator and MotionSensorDecorator decorate LightingComponent to add behaviors
  (dimming control, motion-based auto-on) without modifying Lamp itself.

This file provides:
- LightingComponent (abstract)
- LampTypeFlyweight / LampTypeFlyweightFactory
- Lamp (leaf context)
- DimmingDecorator, MotionSensorDecorator (decorators)
"""

from __future__ import annotations
from abc import ABC, abstractmethod
from typing import Dict, Any, Tuple
import uuid
import threading


class LightingComponent(ABC):
    """Common interface for Composite + Decorator compatibility."""

    @abstractmethod
    def turn_on(self) -> None:
        pass

    @abstractmethod
    def turn_off(self) -> None:
        pass

    @abstractmethod
    def set_brightness(self, percent: float) -> None:
        pass

    @abstractmethod
    def status(self) -> Dict[str, Any]:
        pass


class LampTypeFlyweight:
    """
    Flyweight holding intrinsic, sharable attributes of a lamp type.
    Example intrinsic attributes: bulb_type (LED/HAL), wattage, color_temp.
    """
    __slots__ = ("bulb_type", "wattage_w", "color_temp_k", "life_hours")

    def __init__(self, bulb_type: str, wattage_w: float, color_temp_k: int, life_hours: int = 25000):
        self.bulb_type = bulb_type
        self.wattage_w = float(wattage_w)
        self.color_temp_k = int(color_temp_k)
        self.life_hours = int(life_hours)

    def describe(self) -> Dict[str, Any]:
        return {
            "bulb_type": self.bulb_type,
            "wattage_w": self.wattage_w,
            "color_temp_k": self.color_temp_k,
            "life_hours": self.life_hours,
        }


class LampTypeFlyweightFactory:
    """
    Factory producing and caching LampTypeFlyweight instances.
    Ensures that identical lamp type descriptions reuse the same flyweight.
    """
    _lock = threading.Lock()
    _cache: Dict[Tuple[str, float, int, int], LampTypeFlyweight] = {}

    @classmethod
    def get(cls, bulb_type: str, wattage_w: float, color_temp_k: int, life_hours: int = 25000) -> LampTypeFlyweight:
        key = (bulb_type, float(wattage_w), int(color_temp_k), int(life_hours))
        with cls._lock:
            if key not in cls._cache:
                cls._cache[key] = LampTypeFlyweight(bulb_type, wattage_w, color_temp_k, life_hours)
            return cls._cache[key]

    @classmethod
    def available_types(cls) -> Dict[str, Dict[str, Any]]:
        return {str(k): v.describe() for k, v in cls._cache.items()}


class Lamp(LightingComponent):
    """
    Concrete leaf (Lamp) using a flyweight for intrinsic state.

    Intrinsic state (shared): provided by LampTypeFlyweight
    Extrinsic state (unique per lamp): location, id, current brightness, on/off state
    """

    def __init__(self, location: str, lamp_type: LampTypeFlyweight, identifier: str | None = None):
        self.identifier = identifier or f"lamp-{uuid.uuid4().hex[:8]}"
        self.location = location
        self._type = lamp_type  # Flyweight instance
        self._on = False
        self._brightness = 100.0  # percent (0-100)
        # Track cumulative runtime for simplistic lifetime accounting
        self._runtime_hours = 0.0

    # LightingComponent implementation
    def turn_on(self) -> None:
        if not self._on:
            self._on = True
            print(f"[Lamp:{self.identifier}] ON at {self.location} (type={self._type.bulb_type}).")

    def turn_off(self) -> None:
        if self._on:
            self._on = False
            print(f"[Lamp:{self.identifier}] OFF at {self.location}.")

    def set_brightness(self, percent: float) -> None:
        self._brightness = max(0.0, min(100.0, float(percent)))
        print(f"[Lamp:{self.identifier}] Brightness set to {self._brightness}%.")

    def status(self) -> Dict[str, Any]:
        s = {
            "id": self.identifier,
            "location": self.location,
            "on": self._on,
            "brightness": round(self._brightness, 1),
            "type": self._type.describe(),
            "runtime_hours": round(self._runtime_hours, 2),
        }
        return s

    # Utility: simulate some runtime accumulation (used by controllers in longer simulations)
    def _tick(self, hours: float = 1.0):
        if self._on:
            self._runtime_hours += hours


class LampDecorator(LightingComponent):
    """Base decorator for LightingComponent instances."""

    def __init__(self, wrapped: LightingComponent):
        self._wrapped = wrapped

    def turn_on(self) -> None:
        return self._wrapped.turn_on()

    def turn_off(self) -> None:
        return self._wrapped.turn_off()

    def set_brightness(self, percent: float) -> None:
        return self._wrapped.set_brightness(percent)

    def status(self) -> Dict[str, Any]:
        return self._wrapped.status()


class DimmingDecorator(LampDecorator):
    """
    Adds more nuanced dimming control to a Lamp or any LightingComponent.
    Demonstrates the Decorator pattern.
    """

    def __init__(self, wrapped: LightingComponent, min_dim: float = 10.0):
        super().__init__(wrapped)
        self.min_dim = float(min_dim)

    def set_brightness(self, percent: float) -> None:
        # Enforce min dim level when dimming
        pct = max(self.min_dim, float(percent))
        print(f"[DimmingDecorator] Enforcing min_dim={self.min_dim} -> setting {pct}%")
        self._wrapped.set_brightness(pct)

    def status(self) -> Dict[str, Any]:
        s = self._wrapped.status()
        s["dimming"] = {"min_dim": self.min_dim}
        return s


class MotionSensorDecorator(LampDecorator):
    """
    Auto-on/off based on motion events (simulated).
    The decorator holds a simple sensitivity threshold and a timeout (not a full scheduler).
    """

    def __init__(self, wrapped: LightingComponent, sensitivity: float = 0.5, timeout_secs: int = 60):
        super().__init__(wrapped)
        self.sensitivity = float(sensitivity)
        self.timeout_secs = int(timeout_secs)
        self._last_motion_ts = None  # could be a datetime timestamp in a real system

    def motion_detected(self, intensity: float = 1.0) -> None:
        """Simulate motion event; if above sensitivity then turn on lamp."""
        if float(intensity) >= self.sensitivity:
            print(f"[MotionSensorDecorator] Motion detected (intensity={intensity}) -> turning on.")
            self._wrapped.turn_on()
            # In a real system we'd schedule a delayed off; here it's simulated by developer/test code.

    def turn_on(self) -> None:
        # Allow manual override but log it
        print("[MotionSensorDecorator] Manual turn_on (overrides motion).")
        self._wrapped.turn_on()

    def turn_off(self) -> None:
        print("[MotionSensorDecorator] Manual turn_off.")
        self._wrapped.turn_off()

    def status(self) -> Dict[str, Any]:
        s = self._wrapped.status()
        s["motion_sensor"] = {"sensitivity": self.sensitivity, "timeout_secs": self.timeout_secs}
        return s


# Demo helper
if __name__ == "__main__":
    print("Demo: Lamp + Flyweight + Decorators")
    factory = LampTypeFlyweightFactory
    led_type = factory.get("LED", 15.0, 4000, life_hours=50000)
    hal_type = factory.get("HALOGEN", 60.0, 3000, life_hours=2000)

    # Create two lamps sharing the LED flyweight
    lamp1 = Lamp(location="MainSt-1", lamp_type=led_type, identifier="L-001")
    lamp2 = Lamp(location="MainSt-2", lamp_type=led_type, identifier="L-002")
    lamp3 = Lamp(location="SideAlley-1", lamp_type=hal_type, identifier="L-101")

    # Decorate lamp1 with dimming and motion sensor
    dimmed = DimmingDecorator(lamp1, min_dim=20.0)
    sensor = MotionSensorDecorator(dimmed, sensitivity=0.3)

    print("Turning on sensor-decorated lamp via motion:")
    sensor.motion_detected(intensity=0.5)
    print("Status lamp1:", sensor.status())

    print("\nDirectly turning on lamp2 and setting brightness:")
    lamp2.turn_on()
    lamp2.set_brightness(70)
    print("Status lamp2:", lamp2.status())

    print("\nLamp type cache keys:", LampTypeFlyweightFactory.available_types())