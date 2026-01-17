"""
modules/lighting/lighting_controller.py

LightingController and Zone composite for the lighting subsystem.

Patterns used:
- Composite: Zone acts as a composite container of LightingComponent instances (Lamps or nested Zones).
- Facade: LightingController provides a simple high-level interface (turn_on_zone, set_night_mode, status)
  to orchestrate many lamps/zones.
- The Lamp objects used by zones may use Flyweight and Decorator patterns (implemented in lamp.py).

This controller is intended to be used by the central CityController (Facade) or by tests/CLI.
"""

from __future__ import annotations
from typing import List, Dict, Any, Optional
from lamp import LightingComponent
import threading
import time


class Zone(LightingComponent):
    """
    Composite zone that can contain lamps and nested zones.
    """

    def __init__(self, name: str):
        self.name = name
        self._members: List[LightingComponent] = []

    def add(self, comp: LightingComponent) -> None:
        self._members.append(comp)

    def remove(self, comp: LightingComponent) -> None:
        self._members = [m for m in self._members if m is not comp]

    # LightingComponent interface
    def turn_on(self) -> None:
        print(f"[Zone:{self.name}] Turning ON {len(self._members)} members.")
        for m in self._members:
            m.turn_on()

    def turn_off(self) -> None:
        print(f"[Zone:{self.name}] Turning OFF {len(self._members)} members.")
        for m in self._members:
            m.turn_off()

    def set_brightness(self, percent: float) -> None:
        print(f"[Zone:{self.name}] Setting brightness to {percent}% for {len(self._members)} members.")
        for m in self._members:
            try:
                m.set_brightness(percent)
            except Exception:
                # Some members may not support brightness; ignore gracefully
                continue

    def status(self) -> Dict[str, Any]:
        return {
            "zone": self.name,
            "member_count": len(self._members),
            "members": [m.status() for m in self._members],
        }

    def get_members(self) -> List[LightingComponent]:
        return list(self._members)


class LightingController:
    """
    High-level controller for lighting. Acts as a Facade to the Zone composite structure.

    Responsibilities:
    - Manage top-level zones.
    - Support quick operations: turn on/off zones, set night mode (dim), energy saving profile.
    - Provide status reports aggregated from zones.
    """

    def __init__(self, name: str = "LightingController"):
        self.name = name
        self._zones: Dict[str, Zone] = {}
        self._lock = threading.RLock()
        self._night_mode = False

    def add_zone(self, zone: Zone) -> None:
        with self._lock:
            self._zones[zone.name] = zone
            print(f"[LightingController:{self.name}] Added zone '{zone.name}'.")

    def remove_zone(self, name: str) -> None:
        with self._lock:
            if name in self._zones:
                del self._zones[name]
                print(f"[LightingController:{self.name}] Removed zone '{name}'.")

    def turn_on_zone(self, name: str) -> None:
        zone = self._zones.get(name)
        if not zone:
            print(f"[LightingController:{self.name}] Zone '{name}' not found.")
            return
        zone.turn_on()

    def turn_off_zone(self, name: str) -> None:
        zone = self._zones.get(name)
        if not zone:
            print(f"[LightingController:{self.name}] Zone '{name}' not found.")
            return
        zone.turn_off()

    def set_night_mode(self, enable: bool = True, dim_percent: float = 40.0) -> None:
        """
        Enable/disable night mode. When enabling, dim all zones to dim_percent.
        """
        self._night_mode = bool(enable)
        print(f"[LightingController:{self.name}] Night mode set to {self._night_mode}. Applying dim={dim_percent}%")
        with self._lock:
            for z in self._zones.values():
                z.set_brightness(dim_percent)

    def energy_saving_schedule(self, zone_name: str, dim_percent: float, delay_secs: int):
        """
        Schedule a deferred dim for a zone (simple thread-based scheduler for demo).
        This is a naive implementation for educational/demo purposes only.
        """
        def worker():
            print(f"[LightingController:{self.name}] Scheduled dim in {delay_secs}s for zone '{zone_name}'.")
            time.sleep(delay_secs)
            self.set_night_mode(True, dim_percent)
            print(f"[LightingController:{self.name}] Scheduled dim applied for zone '{zone_name}'.")

        t = threading.Thread(target=worker, daemon=True)
        t.start()

    def status(self) -> Dict[str, Any]:
        with self._lock:
            return {
                "name": self.name,
                "zones": {name: zone.status() for name, zone in self._zones.items()},
                "night_mode": self._night_mode,
            }


# Demo when run directly
if __name__ == "__main__":
    from lamp import LampTypeFlyweightFactory, Lamp, DimmingDecorator, MotionSensorDecorator

    print("Demo: LightingController + Zone + Lamps")
    factory = LampTypeFlyweightFactory
    led = factory.get("LED", 12.0, 3000)
    hal = factory.get("HALOGEN", 60.0, 2700)

    # Create lamps
    l1 = Lamp(location="Plaza-1", lamp_type=led, identifier="PL-001")
    l2 = Lamp(location="Plaza-2", lamp_type=led, identifier="PL-002")
    l3 = Lamp(location="Alley-1", lamp_type=hal, identifier="AL-001")

    # Decorate some lamps
    l1_dec = DimmingDecorator(l1, min_dim=15.0)
    l2_dec = MotionSensorDecorator(l2, sensitivity=0.4)

    # Create zones and controller
    plaza_zone = Zone("Plaza")
    plaza_zone.add(l1_dec)
    plaza_zone.add(l2_dec)

    alley_zone = Zone("Alley")
    alley_zone.add(l3)

    controller = LightingController()
    controller.add_zone(plaza_zone)
    controller.add_zone(alley_zone)

    controller.turn_on_zone("Plaza")
    print("Status after turning on plaza:", controller.status())

    controller.set_night_mode(True, dim_percent=35.0)
    print("Status after night mode:", controller.status())

    print("\nSimulate motion in Plaza-2:")
    # motion decorator only applies to wrapped lamp; attempt to call motion_detected if present
    for member in plaza_zone.get_members():
        try:
            # some decorators expose motion_detected; call if available
            if hasattr(member, "motion_detected"):
                member.motion_detected(intensity=0.8)
        except Exception:
            pass

    controller.turn_off_zone("Plaza")
    print("Final status:", controller.status())