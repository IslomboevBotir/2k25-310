from core.fasctory_method import ModuleCreator


class DummyModule:
    def __init__(self):
        self.initialized = False

    def initialize(self):
        self.initialized = True


class DummyCreator(ModuleCreator):
    def create_module(self):
        return DummyModule()


def test_factory_method_creates_and_initializes_module():
    creator = DummyCreator()
    module = creator.run()

    assert isinstance(module, DummyModule)
    assert module.initialized is True
