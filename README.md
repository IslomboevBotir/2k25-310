text
SmartCity System (Console Application)
=====================================

Overview
--------
SmartCity System is a modular console application that simulates the operation of an intelligent city
management platform. It shows how multiple design patterns can be combined to create a stable,
extensible architecture. Components are intentionally simple and focused on demonstrating patterns,
not production-grade functionality.

Main goals
- Implement at least five design patterns across the architecture.
- Provide a console UI and programmatic tests/demos.
- Organize code into clear modules and packages.

Implemented design patterns (examples)
- Creational:
  - Abstract Factory: core/factories/abstract_factory.py (DayModeFactory, NightModeFactory)
  - Factory Method: core/factories/factory_method.py (VehicleFactory, BusFactory, TramFactory)
  - Builder: core/builders/city_builder.py (CityBuilder, CityDirector)
  - Singleton: core/singleton/config.py and core/controller.CityController uses a singleton metaclass
- Structural:
  - Composite: modules/* use composite patterns (Fleet, Zone, CameraGroup, MeterGroup)
  - Decorator: modules/* provide decorators (ElectricDecorator, DimmingDecorator, AnalyticsDecorator, etc.)
  - Flyweight: modules/lighting/lamp.py and modules/energy/meter.py implement flyweight factories
  - Proxy: core/proxy/access_proxy.py used to protect subsystem access (security demo)
  - Facade: core/controller.CityController brings subsystems together as a facade; many subsystem controllers act as facades too

Project structure
-----------------
FIO/
├── main.py                      # Main application entry point (console UI)
├── test.py                      # Tests / demo scenarios for the system
├── README.md                    # Assignment description and project structure
├── core/                        # Core system components and pattern implementations
│   ├── controller.py
│   ├── factories/
│   ├── builders/
│   ├── adapters/
│   ├── proxy/
│   └── singleton/
├── modules/                     # Smart city subsystems
│   ├── transport/
│   ├── lighting/
│   ├── security/
│   └── energy/

Quick start
-----------
1. Ensure you are running Python 3.9+ (typing features are used).
2. From the project root run demos:

   - Interactive console:
     python main.py
     (or `python main.py night` to set the configured mode before the singleton is created)

   - Automated demos / smoke tests:
     python test.py

Notes and next steps
--------------------
- The provided demo modules emphasize structure and patterns. They can be extended to:
  - Persist data, integrate with real external APIs for weather or telemetry.
  - Add unit tests for each module.
  - Replace print-based logging with a logging framework.
- For classroom/assignment use, look at docstrings in each module which call out the pattern used and its purpose.

License / author
----------------
Educational sample code for demonstrating design patterns in a SmartCity simulation. 