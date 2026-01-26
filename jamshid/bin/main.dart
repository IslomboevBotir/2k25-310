import 'package:main/core/controller/city_facade.dart';
import 'package:main/core/adapters/energy_adapter.dart';
import 'package:main/core/proxy/security_proxy.dart';

void main() {
  print("SmartCity System v1.0 ga xush kelibsiz!");

  var oldGenerator = OldDieselGenerator();
  var powerAdapter = LegacyEnergyAdapter(oldGenerator);

  var secureHub = SecurityProxy("admin123");

  var cityController = SmartCityFacade(powerAdapter, secureHub);

  cityController.startDayMode();

  cityController.startNightMode();

  print("Xakerlik hujumi simulyatsiyasi");
  var hackerProxy = SecurityProxy("wrong_pass");
  hackerProxy.activateLockdown(); 

  cityController.emergencyProtocol();
}