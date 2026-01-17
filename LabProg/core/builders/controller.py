# core/builders/controller.py

class CityDirector:
    """
    Director in the Builder pattern.

    Controls the construction sequence of a SmartCity object.
    """

    def __init__(self, builder):
        self.builder = builder

    def build_minimal_city(self):
        self.builder.build_lighting()
        return self.builder.get_city()

    def build_full_city(self):
        self.builder.build_lighting()
        self.builder.build_transport()
        self.builder.build_security()
        self.builder.build_energy()
        return self.builder.get_city()
