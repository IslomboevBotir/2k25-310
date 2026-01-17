# core/abstract_factory.py

from abc import ABC, abstractmethod


class AbstractCityFactory(ABC):
    """
    Abstract Factory interface.

    Defines methods for creating families of related
    SmartCity subsystems.
    """

    @abstractmethod
    def create_lighting(self):
        pass

    @abstractmethod
    def create_transport(self):
        pass

    @abstractmethod
    def create_security(self):
        pass
