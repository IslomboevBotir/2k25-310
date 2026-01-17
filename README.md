SmartCity System (Dart)

Description

SmartCity System is a console-based application written in Dart that simulates an intelligent city management system.
The system controls several city subsystems such as lighting, transport, and security using object-oriented programming and design patterns.

User interaction is performed through console commands.


Design Patterns Used

The following design patterns are implemented with clear responsibilities:

Singleton — `SmartCityController` ensures a single central controller instance
Facade — `SmartCityController` provides a simplified interface for all subsystems
Abstract Factory — used to create subsystem objects (lighting, transport, security)
Composite — lighting subsystem manages individual lights and groups uniformly
Proxy — controls access and logs operations for lighting and security subsystems
Adapter — `WeatherAdapter` integrates external (mocked) weather data


Project Structure

smartcity_dart/
├── bin/         Entry point
├── lib/         Core logic and subsystems
├── test/        Unit tests
└── pubspec.yaml


How to Run

dart pub get
dart run bin/main.dart
dart test


Console Commands

`status`
`light:on` / `light:off`
`weather`
`transport:start` / `transport:stop`
`security:arm` / `security:disarm`
`exit`

Conclusion

The project demonstrates a modular and extensible Smart City architecture 
using multiple design patterns and fully satisfies the laboratory work requirements.

