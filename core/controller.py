"""
core/controller.py

Central controller for the SmartCity System.

Patterns used:
- Singleton: CityController is a Singleton to ensure a single point of coordination for the city.
- Facade: CityController acts as a Facade, providing a simple interface (start_city, shutdown_city, status_report)
  that coordinates multiple subsystem components created by factories.

This controller depends on the factories in core.factories to build subsystem components.
"""
from typing import Optional

from factories.abstract_factory import DayModeFactory, NightModeFactory, CityModeFactory
from factories.factory_method import BusFactory, TramFactory


class SingletonMeta(type):
    """
    Thread-unsafe Singleton metaclass for simplicity in this educational project.
    Ensures only one instance of CityController exists during runtime.
    """
    _instance: Optional["CityController"] = None

    def __call__(cls, *args, **kwargs):
        if cls._instance is None:
            cls._instance = super().__call__(*args, **kwargs)
        return cls._instance


class CityController(metaclass=SingletonMeta):
    """
    Facade + Singleton.

    Responsibilities:
    - Create subsystem components via an Abstract Factory (Day/Night mode).
    - Use Factory Method for specific product creation (example: additional transport vehicles).
    - Provide high-level operations for the console UI: start, shutdown, status report.
    """

    def __init__(self, mode: str = "day"):
        self.mode_name = mode.lower()
        self.factory: CityModeFactory
        if self.mode_name == "night":
            self.factory = NightModeFactory()
        else:
            self.factory = DayModeFactory()

        # Build subsystems using the chosen factory
        self.lighting = self.factory.create_lighting()
        self.transport = self.factory.create_transport()
        self.security = self.factory.create_security()

        # Example of using Factory Method to create specific transport vehicles
        self.extra_bus = BusFactory().create_vehicle("Bus-42")
        self.extra_tram = TramFactory().create_vehicle("Tram-A")

        self.running = False

    def start_city(self):
        """Start and initialize subsystems (Facade operation)."""
        print(f"[Controller] Starting city in {self.mode_name} mode...")
        self.lighting.turn_on()
        self.transport.initialize()
        self.security.arm()
        self.running = True
        print("[Controller] City started.\n")

    def shutdown_city(self):
        """Shutdown or put subsystems into safe state."""
        print("[Controller] Shutting down city...")
        self.transport.shutdown()
        self.lighting.turn_off()
        self.security.disarm()
        self.running = False
        print("[Controller] City shut down.\n")

    def status_report(self):
        """Collect status from each subsystem and format a simple report."""
        print("[Controller] Generating status report...")
        report = {
            "mode": self.mode_name,
            "running": self.running,
            "lighting": self.lighting.status(),
            "transport": self.transport.status(),
            "security": self.security.status(),
            "extra_bus": self.extra_bus.status(),
            "extra_tram": self.extra_tram.status(),
        }
        for k, v in report.items():
            print(f" - {k}: {v}")
        print()
        return report


# Simple demo when run directly
if __name__ == "__main__":
    controller = CityController(mode="day")
    controller.start_city()
    controller.status_report()
    controller.shutdown_city()