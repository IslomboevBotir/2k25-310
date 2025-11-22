import '../../core/factories/module_factory.dart';

class LightingModule implements Module {
  bool _on = false;

  @override
  void status() {
    print('Lighting is: ' + (_on ? 'ON' : 'OFF'));
  }

  void setLights(bool on) {
    _on = on;
    print('Lighting set to: ' + (_on ? 'ON' : 'OFF'));
  }
}
