# modules/lighting/__init__.py
# Expose lighting subsystem classes for easy import:
# from modules.lighting import LightingController, Zone, Lamp, LampTypeFlyweightFactory, DimmingDecorator, MotionSensorDecorator
from .lighting_controller import LightingController, Zone  # noqa: F401
from .lamp import Lamp, LampTypeFlyweightFactory, DimmingDecorator, MotionSensorDecorator  # noqa: F401

__all__ = [
    "LightingController",
    "Zone",
    "Lamp",
    "LampTypeFlyweightFactory",
    "DimmingDecorator",
    "MotionSensorDecorator",
]