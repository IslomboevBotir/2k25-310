"""
core/adapters/weather_adapter.py

Adapter pattern implementation to integrate an external weather service with the SmartCity internal interface.

Purpose:
- Adapter: WeatherAdapter wraps an ExternalWeatherService (which may have a different API/contract)
  and exposes a stable internal WeatherProvider interface used by the rest of the SmartCity system.
- This isolates the rest of the application from changes in the external service API and allows
  substituting different external providers with minimal changes.

This module includes:
- WeatherProvider: the internal expected interface used by the system.
- ExternalWeatherService: a simulated external API (different contract).
- WeatherAdapter: adapts ExternalWeatherService to WeatherProvider.
"""

from abc import ABC, abstractmethod
from typing import Dict, Any
import random
import datetime


class WeatherProvider(ABC):
    """Internal interface expected by SmartCity components for weather data."""

    @abstractmethod
    def get_temperature_celsius(self) -> float:
        """Return current temperature in Celsius."""
        pass

    @abstractmethod
    def is_rain_expected(self) -> bool:
        """Return True if precipitation is expected shortly."""
        pass

    @abstractmethod
    def get_condition(self) -> str:
        """Return human-readable condition (e.g., 'Sunny', 'Cloudy', 'Rain')."""
        pass

    def get_summary(self) -> Dict[str, Any]:
        """Convenience method that returns a small summary dict."""
        return {
            "temp_c": self.get_temperature_celsius(),
            "condition": self.get_condition(),
            "rain_expected": self.is_rain_expected()
        }


class ExternalWeatherService:
    """
    Simulated external weather service to illustrate an incompatible interface.

    Example external response schema (simulated):
      {
        "tempF": 68.0,
        "desc": "Partly cloudy",
        "precipProbability": 0.15,
        "timestamp": 1600000000
      }

    Real external libraries will have their own methods and response formats.
    """

    def fetch(self) -> Dict[str, Any]:
        """Simulate fetching raw weather data from a third-party provider."""
        # Generate synthetic but plausible values
        temp_c = round(10 + random.random() * 20, 1)
        temp_f = round((temp_c * 9 / 5) + 32, 1)
        precip_prob = round(random.random(), 2)
        desc = random.choice(["Sunny", "Partly cloudy", "Cloudy", "Showers", "Heavy rain"])
        return {
            "tempF": temp_f,
            "description": desc,
            "precipProbability": precip_prob,
            "fetched_at": datetime.datetime.utcnow().timestamp()
        }


class WeatherAdapter(WeatherProvider):
    """
    Adapter that converts ExternalWeatherService responses into the WeatherProvider interface.

    Example adaptation:
      - Converts Fahrenheit to Celsius
      - Interprets precipProbability threshold to boolean rain expectation
      - Maps description fields to internal condition strings
    """

    def __init__(self, external_service: ExternalWeatherService, rain_threshold: float = 0.25):
        self._service = external_service
        self._rain_threshold = rain_threshold
        self._last_raw: Dict[str, Any] = {}

    def _refresh(self):
        """Fetch fresh data from the external service and store raw response."""
        self._last_raw = self._service.fetch()

    def get_temperature_celsius(self) -> float:
        self._refresh()
        raw = self._last_raw
        temp_f = raw.get("tempF")
        if temp_f is None:
            # If provider changed schema, defensively fall back
            raise RuntimeError("External weather response missing 'tempF'")
        temp_c = (temp_f - 32) * 5.0 / 9.0
        return round(temp_c, 1)

    def is_rain_expected(self) -> bool:
        # Use the last raw response if available, otherwise refresh
        if not self._last_raw:
            self._refresh()
        precip = self._last_raw.get("precipProbability", 0)
        return float(precip) >= self._rain_threshold

    def get_condition(self) -> str:
        if not self._last_raw:
            self._refresh()
        # Map external descriptions to a normalized set
        desc = (self._last_raw.get("description") or "").lower()
        if "sun" in desc:
            return "Sunny"
        if "cloud" in desc:
            return "Cloudy"
        if "show" in desc or "rain" in desc:
            return "Rain"
        return "Unknown"

    def get_summary(self) -> Dict[str, Any]:
        # Override to ensure refresh and include raw payload timestamp for debugging
        self._refresh()
        base = super().get_summary()
        base["raw"] = self._last_raw
        return base


# Quick demonstration when run directly
if __name__ == "__main__":
    print("Demo: WeatherAdapter usage")
    external = ExternalWeatherService()
    adapter = WeatherAdapter(external, rain_threshold=0.3)

    print("Temperature (C):", adapter.get_temperature_celsius())
    print("Condition:", adapter.get_condition())
    print("Rain expected?:", adapter.is_rain_expected())
    print("Summary:", adapter.get_summary())