"""
modules/transport/transport_controller.py

TransportController and Fleet (Composite) for the transport subsystem.

Patterns used:
- Composite: Fleet implements a composite container of VehicleComponent instances allowing the
  controller to treat a group of vehicles the same as a single vehicle.
- Facade (informal): TransportController offers a simple interface (initialize, shutdown, status)
  that orchestrates the underlying fleet and vehicles.

The TransportController class is intentionally small and designed to integrate with the
AbstractFactory-based DayTransport/NightTransport implementations if swapped in.
"""

from __future__ import annotations
from typing import List, Dict, Any, Optional
from vehicle import VehicleComponent
import uuid


class Fleet(VehicleComponent):
    """
    Composite that can contain VehicleComponent children (vehicles or nested fleets).
    """

    def __init__(self, name: str = "Fleet"):
        self.name = name
        self._children: List[VehicleComponent] = []

    def add(self, component: VehicleComponent) -> None:
        self._children.append(component)

    def remove(self, component: VehicleComponent) -> None:
        self._children = [c for c in self._children if c is not component]

    def deploy(self) -> None:
        print(f"[Fleet:{self.name}] Deploying {len(self._children)} children...")
        for child in self._children:
            child.deploy()

    def stop(self) -> None:
        print(f"[Fleet:{self.name}] Stopping {len(self._children)} children...")
        for child in self._children:
            child.stop()

    def status(self) -> Dict[str, Any]:
        return {
            "fleet_name": self.name,
            "members": [child.status() for child in self._children],
            "count": len(self._children),
        }

    def get_members(self) -> List[VehicleComponent]:
        return list(self._children)


class TransportController:
    """
    High-level transport controller used by CityController (Facade) or tests.

    Responsibilities:
    - Manage a Fleet of vehicles.
    - Provide initialize(), shutdown(), and status() methods used by the city controller.
    - Provide helpers to add/remove/find vehicles.
    """

    def __init__(self, name: str = "TransportController", default_frequency: str = "normal"):
        self.name = name
        self.fleet = Fleet(name=f"{name}-main")
        self.frequency = default_frequency  # e.g., "high", "normal", "low"
        self.active = False

    def initialize(self) -> None:
        """Start transport services by deploying all fleet members."""
        print(f"[TransportController:{self.name}] Initializing transport services (frequency={self.frequency})...")
        self.fleet.deploy()
        self.active = True
        print(f"[TransportController:{self.name}] Transport initialized.")

    def shutdown(self) -> None:
        """Shutdown transport services by stopping all fleet members."""
        print(f"[TransportController:{self.name}] Shutting down transport services...")
        self.fleet.stop()
        self.active = False
        print(f"[TransportController:{self.name}] Transport shut down.")

    def status(self) -> Dict[str, Any]:
        """Return status summary of transport subsystem."""
        return {
            "name": self.name,
            "active": self.active,
            "frequency": self.frequency,
            "fleet": self.fleet.status(),
        }

    def add_vehicle(self, vehicle: VehicleComponent) -> None:
        """Add a vehicle or nested fleet to the main fleet."""
        self.fleet.add(vehicle)
        print(f"[TransportController:{self.name}] Vehicle added to fleet. Total members: {len(self.fleet.get_members())}")

    def remove_vehicle(self, identifier: str) -> bool:
        """Remove a vehicle by matching its 'id' in status; returns True if removed."""
        for member in list(self.fleet.get_members()):
            try:
                s = member.status()
                vid = s.get("id") or s.get("vehicle_id") or s.get("identifier")
                if vid == identifier:
                    self.fleet.remove(member)
                    print(f"[TransportController:{self.name}] Removed vehicle {identifier}.")
                    return True
            except Exception:
                continue
        return False

    def find_vehicle(self, identifier: str) -> Optional[VehicleComponent]:
        """Find a vehicle by identifier and return the component, or None if not found."""
        for member in self.fleet.get_members():
            try:
                s = member.status()
                if s.get("id") == identifier:
                    return member
            except Exception:
                continue
        return None


# Quick demo when run directly
if __name__ == "__main__":
    from vehicle import Vehicle, ElectricDecorator, GPSDecorator

    controller = TransportController()
    # create two vehicles
    v1 = Vehicle("Bus-100", "Bus", capacity=50)
    v2 = Vehicle("Tram-7", "Tram", capacity=120)
    # decorate: make v1 electric and gps-enabled
    ev1 = ElectricDecorator(v1, battery_level=80.0)
    gps_ev1 = GPSDecorator(ev1, initial_coords=(51.5074, -0.1278))

    controller.add_vehicle(gps_ev1)
    controller.add_vehicle(v2)

    controller.initialize()
    print("Status:", controller.status())
    controller.shutdown()
    print("Final Status:", controller.status())