# modules/transport/__init__.py
# Expose transport subsystem classes for easy import:
# from modules.transport import TransportController, Fleet, Vehicle, ElectricDecorator, GPSDecorator
from .transport_controller import TransportController, Fleet  # noqa: F401
from .vehicle import VehicleComponent, Vehicle, VehicleDecorator, ElectricDecorator, GPSDecorator  # noqa: F401

__all__ = [
    "TransportController",
    "Fleet",
    "VehicleComponent",
    "Vehicle",
    "VehicleDecorator",
    "ElectricDecorator",
    "GPSDecorator",
]