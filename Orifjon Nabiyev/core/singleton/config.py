"""
core/singleton/config.py

Singleton implementation for application-wide configuration and simple in-memory registry.

Purpose:
- Singleton: Config ensures that only a single configuration instance exists across the application.
  It stores settings, provides thread-safe accessors, and can be used by other modules (controller, factories)
  to read global options.

Implementation notes:
- Uses a thread-safe SingletonMeta metaclass with a lock to avoid multiple instantiations in multi-threaded contexts.
- Keeps a simple dictionary-based store and helper methods to load defaults, override values, and retrieve typed values.
"""

from typing import Any, Dict, Optional
import threading


class SingletonMeta(type):
    """Thread-safe Singleton metaclass."""
    _instances: Dict[type, Any] = {}
    _lock: threading.Lock = threading.Lock()

    def __call__(cls, *args, **kwargs):
        # Double-checked locking
        if cls not in cls._instances:
            with cls._lock:
                if cls not in cls._instances:
                    instance = super().__call__(*args, **kwargs)
                    cls._instances[cls] = instance
        return cls._instances[cls]


class Config(metaclass=SingletonMeta):
    """
    Global configuration container and simple registry.

    Example:
        cfg = Config()
        cfg.set("mode", "day")
        cfg.get("mode") -> "day"
    """
    def __init__(self, defaults: Optional[Dict[str, Any]] = None):
        self._data: Dict[str, Any] = {}
        self._lock = threading.RLock()
        if defaults:
            self._data.update(defaults)

    def get(self, key: str, default: Any = None) -> Any:
        """Retrieve a value for 'key' or return default if not present."""
        with self._lock:
            return self._data.get(key, default)

    def set(self, key: str, value: Any):
        """Set a configuration key to value."""
        with self._lock:
            self._data[key] = value

    def update(self, mapping: Dict[str, Any]):
        """Bulk update configuration from a mapping."""
        with self._lock:
            self._data.update(mapping)

    def as_dict(self) -> Dict[str, Any]:
        """Return a shallow copy of current config data."""
        with self._lock:
            return dict(self._data)

    def load_defaults(self):
        """Populate sensible defaults for the SmartCity demo (can be extended)."""
        with self._lock:
            defaults = {
                "mode": "day",
                "log_level": "INFO",
                "enable_energy_saving": True,
                "admin_roles": ["admin", "operator"],
            }
            for k, v in defaults.items():
                self._data.setdefault(k, v)

    # Convenience typed getters
    def get_bool(self, key: str, default: bool = False) -> bool:
        return bool(self.get(key, default))

    def get_int(self, key: str, default: int = 0) -> int:
        try:
            return int(self.get(key, default))
        except (TypeError, ValueError):
            return default


# Demonstration helper
if __name__ == "__main__":
    print("Demo: Config singleton usage")
    c1 = Config()
    c1.load_defaults()
    print("Config snapshot:", c1.as_dict())

    c2 = Config()
    print("Second reference equals first:", c1 is c2)

    c2.set("mode", "night")
    print("c1.get('mode') ->", c1.get("mode"))
    print("All data:", c1.as_dict())