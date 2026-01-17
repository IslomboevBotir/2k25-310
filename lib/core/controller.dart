// lib/core/controller.dart


import 'package:smart_city/core/proxy/subsystem_proxy.dart';

import '../modules/lighting/lighting.dart';
import '../modules/security/security.dart';
import '../modules/transport/transport.dart';
import 'adapters/weather_adapter.dart';
import 'factories/subsystem_factory.dart';

/// SmartCityController
/// - Implemented as a **Singleton**: only one instance exists application-wide.
/// - Acts as a **Facade**, providing a simplified API to the console UI and coordinating subsystems.
class SmartCityController {
  SmartCityController._internal() {
    // Initialize subsystems via Abstract Factory
    final factory = DefaultSubsystemFactory();
    _lighting = factory.createLightingSubsystem();
    _transport = factory.createTransportSubsystem();
    _security = factory.createSecuritySubsystem();

    // Use Proxy to add an access/logging layer to sensitive subsystems (lighting, security)
    _lightingProxy = SubsystemProxy(_lighting);
    _securityProxy = SubsystemProxy(_security);

    // Adapter to external/mock services
    _weather = WeatherAdapter();
  }

  static final SmartCityController _instance = SmartCityController._internal();
  static SmartCityController get instance => _instance;

  late LightingSubsystem _lighting;
  late TransportSubsystem _transport;
  late SecuritySubsystem _security;

  late SubsystemProxy _lightingProxy;
  late SubsystemProxy _securityProxy;

  late WeatherAdapter _weather;

  void printStatus() {
    print('--- System Status ---');
    print('Lighting: ${_lighting.status()}');
    print('Transport: ${_transport.status()}');
    print('Security: ${_security.status()}');
  }

  void enableLighting() {
    _lightingProxy.start();
  }

  void disableLighting() {
    _lightingProxy.stop();
  }

  Future<void> fetchWeatherAndAdjustLighting() async {
    final condition = await _weather.getCurrentCondition();
    print('Weather: $condition');
    if (condition == 'rain') {
      print('It is rainy — setting lighting to high brightness (scene).');
      _lighting.setScene('high');
    } else {
      _lighting.setScene('normal');
    }
  }

  void startTransport() => _transport.start();
  void stopTransport() => _transport.stop();

  void armSecurity() => _securityProxy.start();
  void disarmSecurity() => _securityProxy.stop();
}
