import '../../modules/transport/transport_module.dart';
import '../../modules/lighting/lighting_module.dart';
import '../../modules/security/security_module.dart';
import '../../modules/energy/energy_module.dart';

abstract class Module {
  void status();
}

class ModuleFactory {
  static Module create(String name) {
    switch (name) {
      case 'transport':
        return TransportModule();
      case 'lighting':
        return LightingModule();
      case 'security':
        return SecurityModule();
      case 'energy':
        return EnergyModule();
      default:
        throw UnsupportedError('Unknown module: \$name');
    }
  }
}
