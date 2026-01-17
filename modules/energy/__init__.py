# modules/energy/__init__.py
# Expose energy subsystem classes for easy import:
# from modules.energy import EnergyController, MeterGroup, Meter, MeterTypeFlyweightFactory, ConsumptionDecorator
from .energy_controller import EnergyController, MeterGroup  # noqa: F401
from .meter import MeterComponent, Meter, MeterTypeFlyweightFactory, ConsumptionDecorator  # noqa: F401

__all__ = [
    "EnergyController",
    "MeterGroup",
    "MeterComponent",
    "Meter",
    "MeterTypeFlyweightFactory",
    "ConsumptionDecorator",
]