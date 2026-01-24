import pytest
from core.abstract_factory import AbstractCityFactory


def test_abstract_factory_cannot_be_instantiated():
    with pytest.raises(TypeError):
        AbstractCityFactory()


class DummyFactory(AbstractCityFactory):
    def create_lighting(self):
        return "Light"

    def create_transport(self):
        return "Transport"

    def create_security(self):
        return "Security"


def test_abstract_factory_methods():
    factory = DummyFactory()

    assert factory.create_lighting() == "Light"
    assert factory.create_transport() == "Transport"
    assert factory.create_security() == "Security"
