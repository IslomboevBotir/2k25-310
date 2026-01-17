# core/adapters/__init__.py
# Export adapter classes for easy import: from core.adapters import WeatherAdapter, ExternalWeatherService
from .weather_adapter import WeatherProvider, ExternalWeatherService, WeatherAdapter  # noqa: F401

__all__ = ["WeatherProvider", "ExternalWeatherService", "WeatherAdapter"]