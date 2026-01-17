"""
core/factories/factory_method.py

Implementation of the Factory Method pattern for specific products in SmartCity.

Purpose:
- The Factory Method pattern provides an interface for creating an object, but lets subclasses decide
  which class to instantiate. Here we use it for creating specific transport vehicles (Bus, Tram).
- This complements the Abstract Factory: while Abstract Factory creates families of subsystems (lighting/transport/security),
  Factory Method handles creating specific concrete products within one subsystem (e.g., different vehicle types).

This module provides:
- Vehicle (Product) interface and concrete Bus/Tram implementations.
- VehicleFactory (Creator) abstract class and concrete factories: BusFactory, TramFactory.
"""

from abc import ABC, abstractmethod


# --- Product interface and concrete products --- #
class Vehicle(ABC):
    def __init__(self, identifier: str):
        self.identifier = identifier
        self.operational = False

    @abstractmethod
    def deploy(self):
        pass

    def status(self) -> dict:
        return {"id": self.identifier, "operational": self.operational, "type": self.__class__.__name__}


class Bus(Vehicle):
    def deploy(self):
        self.operational = True
        print(f"[Bus] Bus {self.identifier} deployed on its route.")


class Tram(Vehicle):
    def deploy(self):
        self.operational = True
        print(f"[Tram] Tram {self.identifier} started service on its line.")


# --- Creator (Factory Method) --- #
class VehicleFactory(ABC):
    """
    Creator declares the factory method that returns a Vehicle.
    Subclasses implement the factory method to return concrete products.
    """
    @abstractmethod
    def create_vehicle(self, identifier: str) -> Vehicle:
        pass


class BusFactory(VehicleFactory):
    def create_vehicle(self, identifier: str) -> Vehicle:
        vehicle = Bus(identifier)
        # Potential additional Bus initialization logic goes here
        vehicle.deploy()
        return vehicle


class TramFactory(VehicleFactory):
    def create_vehicle(self, identifier: str) -> Vehicle:
        vehicle = Tram(identifier)
        # Potential additional Tram initialization logic goes here
        vehicle.deploy()
        return vehicle


# Simple demo when run directly
if __name__ == "__main__":
    print("Demo: Factory Method usage")
    bf = BusFactory()
    tf = TramFactory()

    b = bf.create_vehicle("Bus-99")
    t = tf.create_vehicle("Tram-Z")


    print(b.status())
    print(t.status())