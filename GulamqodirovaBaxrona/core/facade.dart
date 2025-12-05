import '../modules/lighting.dart';
import '../modules/transport.dart';
import '../modules/security.dart';
import '../modules/energy.dart';

/// Facade — упрощённый интерфейс для управления подсистемами города
class SmartCityFacade {
  final LightingSystem _light = LightingSystem();
  final TransportSystem _transport = TransportSystem();
  final SecuritySystem _security = SecuritySystem();
  final EnergyProxy _energy = EnergyProxy('admin');

  List<String> morningMode() => [
    _light.turnOff(),
    _transport.run(),
    _security.scan(),
  ];

  List<String> nightMode(String pwd) => [
    _light.turnOn(),
    _security.scan(),
    _energy.getUsage(pwd),
  ];
}