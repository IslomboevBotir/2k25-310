"""
core/builders/city_builder.py

Builder pattern implementation for constructing a City object step-by-step.

Purpose:
- Builder: CityBuilder provides a fluent interface to assemble a City composed of
  multiple subsystems (lighting, transport, security, energy, weather provider, etc.).
  This is useful for creating different configurations (e.g., day mode, night mode,
  energy-saving profiles) without exposing complex construction details to the client.

- Director: CityDirector encapsulates common construction sequences (e.g., standard day
  configuration, night configuration) using a builder.

Notes:
- This builder is intentionally lightweight and coordinates with the AbstractFactory
  implementations (core.factories.abstract_factory) for subsystem creation when requested.
- Each builder method returns self to allow method chaining.
"""

from dataclasses import dataclass, field
from typing import Optional, List, Any

# Avoid circular heavy imports; use typing for CityModeFactory
try:
    from core.factories.abstract_factory import CityModeFactory
except Exception:
    # If used in isolation (for tests), define a minimal protocol substitute
    class CityModeFactory:  # type: ignore
        def create_lighting(self):
            raise NotImplementedError

        def create_transport(self):
            raise NotImplementedError

        def create_security(self):
            raise NotImplementedError


@dataclass
class City:
    """Simple container for assembled city components."""
    name: str = "SmartCity"
    lighting: Optional[Any] = None
    transport: Optional[Any] = None
    security: Optional[Any] = None
    energy: Optional[Any] = None
    weather_provider: Optional[Any] = None
    additional_vehicles: List[Any] = field(default_factory=list)
    metadata: dict = field(default_factory=dict)

    def summary(self) -> dict:
        """Return a concise summary used by status reporters / controllers."""
        return {
            "name": self.name,
            "lighting": getattr(self.lighting, "status", lambda: "n/a")()
            if self.lighting else None,
            "transport": getattr(self.transport, "status", lambda: "n/a")()
            if self.transport else None,
            "security": getattr(self.security, "status", lambda: "n/a")()
            if self.security else None,
            "energy": getattr(self.energy, "status", lambda: "n/a")()
            if self.energy else None,
            "weather": getattr(self.weather_provider, "get_summary", lambda: "n/a")()
            if self.weather_provider else None,
            "extra_vehicles": [getattr(v, "status", lambda: {})() for v in self.additional_vehicles],
            "metadata": self.metadata,
        }


class CityBuilder:
    """
    Builder for City objects.

    Usage example:
        builder = CityBuilder("MyCity")
        city = (builder
                .with_mode(day_factory)            # uses AbstractFactory to populate lighting/transport/security
                .with_weather_provider(adapter)    # adapter implementing weather interface
                .enable_energy_module(energy_obj)
                .add_vehicle(vehicle_obj)          # optional additional vehicles created by Factory Method
                .set_metadata("population", 100000)
                .build())
    """

    def __init__(self, name: str = "SmartCity"):
        self._city = City(name=name)

    def with_mode(self, factory: CityModeFactory) -> "CityBuilder":
        """Use an Abstract Factory to provide the standard family of subsystems for the configured mode."""
        if factory is None:
            return self
        self._city.lighting = factory.create_lighting()
        self._city.transport = factory.create_transport()
        self._city.security = factory.create_security()
        # Keep a reference to the factory in metadata for debugging / reconstruction
        self._city.metadata["mode_factory"] = factory.__class__.__name__
        return self

    def with_lighting(self, lighting_obj) -> "CityBuilder":
        """Attach a specific lighting implementation (overrides factory lighting)."""
        self._city.lighting = lighting_obj
        return self

    def with_transport(self, transport_obj) -> "CityBuilder":
        """Attach a specific transport implementation (overrides factory transport)."""
        self._city.transport = transport_obj
        return self

    def with_security(self, security_obj) -> "CityBuilder":
        """Attach a specific security implementation (overrides factory security)."""
        self._city.security = security_obj
        return self

    def enable_energy_module(self, energy_obj) -> "CityBuilder":
        """Attach an energy monitoring/saving module."""
        self._city.energy = energy_obj
        self._city.metadata["energy_enabled"] = True
        return self

    def with_weather_provider(self, weather_provider) -> "CityBuilder":
        """Attach a weather provider (adapter implementing the expected interface)."""
        self._city.weather_provider = weather_provider
        return self

    def add_vehicle(self, vehicle_obj) -> "CityBuilder":
        """Add extra vehicles (created with Factory Method) to the city."""
        self._city.additional_vehicles.append(vehicle_obj)
        return self

    def set_metadata(self, key: str, value) -> "CityBuilder":
        """Set arbitrary metadata on the city (e.g., population, admin contact)."""
        self._city.metadata[key] = value
        return self

    def build(self) -> City:
        """Finalize and return the constructed City object."""
        # Perform light validation: ensure required subsystems exist
        if not (self._city.lighting and self._city.transport and self._city.security):
            raise ValueError("City must have lighting, transport and security subsystems configured.")
        return self._city


class CityDirector:
    """
    Director that defines common construction sequences for CityBuilder.

    This keeps construction logic in one place and makes it easy to produce
    commonly used variants:
      - standard_day_city(builder, extra_vehicles=[])
      - energy_saving_night_city(builder, energy_module, weather_adapter)
    """

    @staticmethod
    def standard_day_city(builder: CityBuilder, factory: CityModeFactory, extra_vehicles: list = None, weather_provider=None):
        b = builder.with_mode(factory)
        if weather_provider:
            b.with_weather_provider(weather_provider)
        if extra_vehicles:
            for v in extra_vehicles:
                b.add_vehicle(v)
        b.set_metadata("profile", "standard_day")
        return b.build()

    @staticmethod
    def energy_saving_night_city(builder: CityBuilder, factory: CityModeFactory, energy_module, weather_provider=None):
        b = builder.with_mode(factory).enable_energy_module(energy_module)
        if weather_provider:
            b.with_weather_provider(weather_provider)
        b.set_metadata("profile", "energy_saving_night")
        return b.build()


# Demonstration helper
if __name__ == "__main__":
    # A minimal demo that uses the DayModeFactory from core.factories if available.
    try:
        from factories.abstract_factory import DayModeFactory
        from core.factories.factory_method import BusFactory
    except Exception:
        DayModeFactory = None
        BusFactory = None

    print("Demo: CityBuilder usage")
    builder = CityBuilder("DemoCity")
    if DayModeFactory:
        factory = DayModeFactory()
        city = CityDirector.standard_day_city(builder, factory, extra_vehicles=[BusFactory().create_vehicle("Demo-Bus")] if BusFactory else None)
        print(city.summary())
    else:
        print("DayModeFactory not available in this context; builder demo skipped.")