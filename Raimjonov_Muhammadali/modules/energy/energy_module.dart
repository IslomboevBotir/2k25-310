import '../../core/factories/module_factory.dart';

class EnergyModule implements Module {
  double _consumption = 123.45;

  @override
  void status() {
    print('Energy consumption today: \$_consumption kWh');
  }
}
