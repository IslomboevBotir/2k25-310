# SmartCity System (Console, PHP)

## Patterns used (7):
1. Singleton - SmartCityController (single central controller)
2. Facade - SmartCityController provides unified interface to subsystems
3. Abstract Factory - creates subsystems consistently (DefaultSubsystemFactory)
4. Adapter - WeatherServiceAdapter wraps ExternalWeatherApi
5. Proxy - SecurityProxy controls access (admin-only arm/disarm)
6. Composite - Lighting zones contain multiple lights
7. Decorator - EnergySavingLightingDecorator adds energy-saving behavior

## Run
- php main.php
- php test.php
