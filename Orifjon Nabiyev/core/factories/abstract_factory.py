"""
core/factories/abstract_factory.py

Implementation of the Abstract Factory pattern for the SmartCity System.

Purpose:
- AbstractFactory defines an interface for creating families of related subsystems (lighting, transport, security)
  that should be used together (e.g., Day vs Night mode).
- DayModeFactory and NightModeFactory produce appropriate concrete subsystem implementations
  configured for the selected city mode.

Each produced object implements a minimal interface used by CityController (facade).
"""

from abc import ABC, abstractmethod
from typing import Dict


# --- Subsystem component interfaces / lightweight implementations --- #
class Lighting(ABC):
    @abstractmethod
    def turn_on(self):
        pass

    @abstractmethod
    def turn_off(self):
        pass

    @abstractmethod
    def status(self) -> Dict:
        pass


class Transport(ABC):
    @abstractmethod
    def initialize(self):
        pass

    @abstractmethod
    def shutdown(self):
        pass

    @abstractmethod
    def status(self) -> Dict:
        pass


class Security(ABC):
    @abstractmethod
    def arm(self):
        pass

    @abstractmethod
    def disarm(self):
        pass

    @abstractmethod
    def status(self) -> Dict:
        pass


# --- Concrete implementations for Day --- #
class DayLighting(Lighting):
    def __init__(self):
        self.on = False
        self.brightness = 100  # percent

    def turn_on(self):
        self.on = True
        print("[DayLighting] Ambient/Street lighting set to high brightness.")

    def turn_off(self):
        self.on = False
        print("[DayLighting] Turning off daytime auxiliary lighting.")

    def status(self) -> Dict:
        return {"type": "DayLighting", "on": self.on, "brightness": self.brightness}


class DayTransport(Transport):
    def __init__(self):
        self.active = False
        self.frequency = "high"

    def initialize(self):
        self.active = True
        print("[DayTransport] Activating daytime transport with high frequency.")

    def shutdown(self):
        self.active = False
        print("[DayTransport] Reducing daytime transport operations.")

    def status(self) -> Dict:
        return {"type": "DayTransport", "active": self.active, "frequency": self.frequency}


class DaySecurity(Security):
    def __init__(self):
        self.armed = True
        self.sensitivity = "normal"

    def arm(self):
        self.armed = True
        print("[DaySecurity] Security armed (day mode).")

    def disarm(self):
        self.armed = False
        print("[DaySecurity] Security disarmed (day mode).")

    def status(self) -> Dict:
        return {"type": "DaySecurity", "armed": self.armed, "sensitivity": self.sensitivity}


# --- Concrete implementations for Night --- #
class NightLighting(Lighting):
    def __init__(self):
        self.on = False
        self.brightness = 40  # percent; energy saving at night

    def turn_on(self):
        self.on = True
        print("[NightLighting] Street lighting dimmed for energy saving.")

    def turn_off(self):
        self.on = False
        print("[NightLighting] Turning off selective lighting.")

    def status(self) -> Dict:
        return {"type": "NightLighting", "on": self.on, "brightness": self.brightness}


class NightTransport(Transport):
    def __init__(self):
        self.active = False
        self.frequency = "low"

    def initialize(self):
        self.active = True
        print("[NightTransport] Activating reduced-night transport schedule.")

    def shutdown(self):
        self.active = False
        print("[NightTransport] Night transport services stopped.")

    def status(self) -> Dict:
        return {"type": "NightTransport", "active": self.active, "frequency": self.frequency}


class NightSecurity(Security):
    def __init__(self):
        self.armed = True
        self.sensitivity = "high"  # higher sensitivity at night

    def arm(self):
        self.armed = True
        print("[NightSecurity] Security armed with high sensitivity.")

    def disarm(self):
        self.armed = False
        print("[NightSecurity] Security disarmed (night mode).")

    def status(self) -> Dict:
        return {"type": "NightSecurity", "armed": self.armed, "sensitivity": self.sensitivity}


# --- Abstract Factory Interface --- #
class CityModeFactory(ABC):
    """
    Abstract Factory interface: declares methods to create each subsystem in a coherent family.
    """
    @abstractmethod
    def create_lighting(self) -> Lighting:
        pass

    @abstractmethod
    def create_transport(self) -> Transport:
        pass

    @abstractmethod
    def create_security(self) -> Security:
        pass


# --- Concrete Factories --- #
class DayModeFactory(CityModeFactory):
    """
    DayModeFactory creates subsystems configured for daytime operation.
    """
    def create_lighting(self) -> Lighting:
        return DayLighting()

    def create_transport(self) -> Transport:
        return DayTransport()

    def create_security(self) -> Security:
        return DaySecurity()


class NightModeFactory(CityModeFactory):
    """
    NightModeFactory creates subsystems configured for nighttime operation.
    """
    def create_lighting(self) -> Lighting:
        return NightLighting()

    def create_transport(self) -> Transport:
        return NightTransport()

    def create_security(self) -> Security:
        return NightSecurity()


# Quick demonstration helper
if __name__ == "__main__":
    print("Demo: Abstract Factory usage")
    day_factory = DayModeFactory()
    day_lighting = day_factory.create_lighting()
    day_transport = day_factory.create_transport()
    day_security = day_factory.create_security()

    day_lighting.turn_on()
    day_transport.initialize()
    day_security.arm()

    print(day_lighting.status())
    print(day_transport.status())
    print(day_security.status())