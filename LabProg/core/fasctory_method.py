# core/fasctory_method.py

from abc import ABC, abstractmethod


class ModuleCreator(ABC):
    """
    Factory Method base class.

    Subclasses decide which concrete module to instantiate.
    """

    @abstractmethod
    def create_module(self):
        pass

    def run(self):
        """
        Common logic using the factory method.
        """
        module = self.create_module()
        module.initialize()
        return module
    