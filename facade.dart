import 'package:main/modules/energy.dart';
import 'package:main/modules/lighting.dart';
import 'package:main/modules/security.dart';

class CityControlPanel {
  final _lights = LightingService();
  final _cameras = CameraService();
  final _energy = EnergyGrid();
  
  void activateNightMode() {
    print('\nTungi rejim ishga tushirilmoqda');
    _energy.ecoMode(true);
    _lights.turnOn();
    _cameras.startRecording();
    print('Status: OK\n');
  }

  void activateDayMode() {
    print('\nKunduzgi rejim');
    _lights.turnOff();
    _cameras.stopRecording();
    _energy.ecoMode(false);
    print('Status: OK\n');
  }

  void emergencyAlert() {
    print('XAVF ANIQALANDI');
    _lights.turnOn();
    _cameras.triggerAlarm();
    _energy.ecoMode(false);
    print('Xizmatlarga xabar berildi');
  }
}