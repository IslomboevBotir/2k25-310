import '../modules/energy/energy.dart';
import '../modules/lighting/lighting.dart';
import '../modules/security/security.dart';
import '../modules/transport/transport.dart';

class CityController {
  static final CityController _instance = CityController._internal();
  factory CityController() => _instance;
  CityController._internal();

  Lighting? lighting;
  Energy? energy;
  Transport? transport;
  Security? security;

  void setupSity() {
    print('Seeing UP!');
    lighting?.turnOn();
    transport?.startBuses();
    security?.activateCameras();
    energy?.saveEnergy();
  }


  void shutDownCity() {
    print('Shutting DOWN!');
    lighting?.turnOff();
    transport?.stopBuses();
    security?.deactivateCameras();
    energy?.normalMode();
  }
}
