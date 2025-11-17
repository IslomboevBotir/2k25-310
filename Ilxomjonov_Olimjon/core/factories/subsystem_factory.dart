import '../../modules/energy/energy.dart';
import '../../modules/lighting/lighting.dart';
import '../../modules/security/security.dart';
import '../../modules/transport/transport.dart';

abstract class SubsystemFactory {
  dynamic createSubSysmte();
}

class LightingFactory implements SubsystemFactory {
  @override
  Lighting createSubSysmte() => Lighting();
}

class TransportFactory implements SubsystemFactory {
  @override
  Transport createSubSysmte() => Transport();
}

class SecurityFactory implements SubsystemFactory {
  @override
  Security createSubSysmte() => Security();
}


class EnergyFactory implements SubsystemFactory{
  @override
  Energy createSubSysmte() => Energy();
}