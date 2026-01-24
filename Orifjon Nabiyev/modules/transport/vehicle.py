"""
modules/transport/vehicle.py

Transport vehicle components.

Patterns used:
- Composite: VehicleComponent is the common interface; Vehicle (leaf) and Fleet (composite) may be used
  together so the transport controller can treat individual vehicles and groups uniformly.
- Decorator: VehicleDecorator wraps VehicleComponent to add features (e.g., ElectricDecorator, GPSDecorator)
  without changing the underlying Vehicle class.

This module defines a lightweight vehicle model intended for the transport subsystem.
"""

from __future__ import annotations
from abc import ABC, abstractmethod
from typing import Dict, Any, List


class VehicleComponent(ABC):
    """Abstract component for Composite pattern used in transport subsystem."""

    @abstractmethod
    def deploy(self) -> None:
        """Start or deploy the vehicle (or subtree)."""
        pass

    @abstractmethod
    def stop(self) -> None:
        """Stop the vehicle (or subtree)."""
        pass

    @abstractmethod
    def status(self) -> Dict[str, Any]:
        """Return a status dictionary describing the vehicle or composite."""
        pass


class Vehicle(VehicleComponent):
    """Concrete leaf representing a single vehicle."""

    def __init__(self, identifier: str, vehicle_type: str = "Generic", capacity: int = 0):
        self.identifier = identifier
        self.vehicle_type = vehicle_type
        self.capacity = capacity
        self.operational = False
        self.location = "depot"

    def deploy(self) -> None:
        self.operational = True
        self.location = "on-route"
        print(f"[Vehicle:{self.identifier}] {self.vehicle_type} deployed (capacity={self.capacity}).")

    def stop(self) -> None:
        self.operational = False
        self.location = "depot"
        print(f"[Vehicle:{self.identifier}] {self.vehicle_type} stopped and returned to depot.")

    def status(self) -> Dict[str, Any]:
        return {
            "id": self.identifier,
            "type": self.vehicle_type,
            "capacity": self.capacity,
            "operational": self.operational,
            "location": self.location,
        }


class VehicleDecorator(VehicleComponent):
    """
    Base Decorator for VehicleComponent.

    Wraps another VehicleComponent and can extend behavior.
    """

    def __init__(self, wrapped: VehicleComponent):
        self._wrapped = wrapped

    def deploy(self) -> None:
        return self._wrapped.deploy()

    def stop(self) -> None:
        return self._wrapped.stop()

    def status(self) -> Dict[str, Any]:
        return self._wrapped.status()


class ElectricDecorator(VehicleDecorator):
    """
    Decorator that adds simple electric-specific attributes (battery_level).
    Demonstrates the Decorator pattern to add responsibilities at runtime.
    """

    def __init__(self, wrapped: VehicleComponent, battery_level: float = 100.0):
        super().__init__(wrapped)
        # battery level between 0 and 100
        self.battery_level = float(max(0.0, min(100.0, battery_level)))

    def deploy(self) -> None:
        # Prevent deploy if battery is too low
        if self.battery_level < 10.0:
            print(f"[ElectricDecorator:{getattr(self._wrapped, 'identifier', 'unknown')}] Cannot deploy: battery too low ({self.battery_level}%).")
            return
        print(f"[ElectricDecorator:{getattr(self._wrapped, 'identifier', 'unknown')}] Battery level: {self.battery_level}%. Proceeding to deploy.")
        self._wrapped.deploy()
        # Simulate small battery usage on deploy
        self.battery_level = max(0.0, self.battery_level - 5.0)

    def stop(self) -> None:
        self._wrapped.stop()
        # Simulate battery recharge on stop (simple heuristic)
        self.battery_level = min(100.0, self.battery_level + 2.0)
        print(f"[ElectricDecorator:{getattr(self._wrapped, 'identifier', 'unknown')}] Charging: battery now {self.battery_level}%.")

    def status(self) -> Dict[str, Any]:
        s = self._wrapped.status()
        s["battery_level"] = round(self.battery_level, 1)
        s["electric"] = True
        return s


class GPSDecorator(VehicleDecorator):
    """
    Decorator that adds GPS capability: current coordinates and a simple tracker.
    """

    def __init__(self, wrapped: VehicleComponent, initial_coords: tuple = (0.0, 0.0)):
        super().__init__(wrapped)
        self.coords = tuple(float(x) for x in initial_coords)

    def deploy(self) -> None:
        print(f"[GPSDecorator:{getattr(self._wrapped, 'identifier', 'unknown')}] Activating GPS at coords {self.coords}.")
        self._wrapped.deploy()

    def stop(self) -> None:
        self._wrapped.stop()
        print(f"[GPSDecorator:{getattr(self._wrapped, 'identifier', 'unknown')}] GPS recording stopped.")

    def update_coords(self, lat: float, lon: float) -> None:
        self.coords = (float(lat), float(lon))
        print(f"[GPSDecorator:{getattr(self._wrapped, 'identifier', 'unknown')}] GPS updated to {self.coords}.")

    def status(self) -> Dict[str, Any]:
        s = self._wrapped.status()
        s["gps_coords"] = self.coords
        s["gps_enabled"] = True
        return s


# Note: Fleet composite is implemented in transport_controller.py to keep controller logic together.
if __name__ == "__main__":
    # Quick demo of vehicle + decorators
    v = Vehicle("V-1", "Bus", capacity=40)
    ev = ElectricDecorator(v, battery_level=50.0)
    gps_ev = GPSDecorator(ev, initial_coords=(40.7128, -74.0060))

    print("Status before deploy:", gps_ev.status())
    gps_ev.deploy()
    gps_ev.update_coords(40.7130, -74.0058)
    print("Status after deploy:", gps_ev.status())
    gps_ev.stop()
    print("Final status:", gps_ev.status())