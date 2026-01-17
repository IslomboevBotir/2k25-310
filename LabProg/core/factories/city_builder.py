# core/factories/city_builder.py

class SmartCity:
    """
    Product being built.
    """

    def __init__(self):
        self.components = []

    def add_component(self, component):
        self.components.append(component)

    def describe(self):
        return f"SmartCity with components: {', '.join(self.components)}"


class CityBuilder:
    """
    Concrete Builder implementation.
    """

    def __init__(self):
        self.city = SmartCity()

    def build_lighting(self):
        self.city.add_component("Lighting System")

    def build_transport(self):
        self.city.add_component("Transport System")

    def build_security(self):
        self.city.add_component("Security System")

    def build_energy(self):
        self.city.add_component("Energy System")

    def get_city(self):
        return self.city
