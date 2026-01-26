import '../singleton/city_config.dart';
import '../factories/sensor_factory.dart';
import '../adapters/energy_adapter.dart';
import '../proxy/security_proxy.dart';

class SmartCityFacade {
  final CityConfig _config = CityConfig();
  final IEnergySource _energySource;
  final ISecuritySystem _securitySystem;

  SmartCityFacade(this._energySource, this._securitySystem);

  void startDayMode() {
    print("KUNDUZGI REJIM BOSHLANDI");
    _config.setTime(true);
    _energySource.supplyPower();
    
    Sensor traffic = SensorFactory.createSensor('traffic');
    print(traffic.collectData());
  }

  void startNightMode() {
    print("TUNGI REJIM BOSHLANDI");
    _config.setTime(false);
    
    Sensor env = SensorFactory.createSensor('env');
    print(env.collectData());
  }

  void emergencyProtocol() {
    print("FAVQULODDA HOLAT");
    _securitySystem.activateLockdown();
  }
}