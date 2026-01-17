from core.builders.controller import CityDirector
from core.factories.city_builder import CityBuilder, SmartCity


def test_city_builder_minimal_city():
    builder = CityBuilder()
    director = CityDirector(builder)

    city = director.build_minimal_city()

    assert isinstance(city, SmartCity)
    assert city.components == ["Lighting System"]


def test_city_builder_full_city():
    builder = CityBuilder()
    director = CityDirector(builder)

    city = director.build_full_city()

    assert city.components == [
        "Lighting System",
        "Transport System",
        "Security System",
        "Energy System",
    ]


def test_city_description():
    builder = CityBuilder()
    builder.build_lighting()
    builder.build_energy()

    city = builder.get_city()
    description = city.describe()

    assert "Lighting System" in description
    assert "Energy System" in description
