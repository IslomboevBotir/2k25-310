import 'core/builders/report_builder.dart';
import 'core/controller.dart';
import 'core/factories/subsystem_factory.dart';
import 'core/proxy/security_proxy.dart';

void main() {
  // Factories
  var lighting = LightingFactory().createSubSysmte();
  var transport = TransportFactory().createSubSysmte();
  var energy = EnergyFactory().createSubSysmte();
  var security = SecurityFactory().createSubSysmte();

  // Controllers
  var controller = CityController();
  controller.lighting = lighting;
  controller.transport = transport;
  controller.energy = energy;
  controller.security = security;
  print('////////////////////////');

  // Facade ON
  controller.setupSity();
  print('////////////////////////');
  // Proxy
  var securityProxy = SecurityProxy();
  securityProxy.activateCamera();
  securityProxy.isAdmin = true;
  securityProxy.activateCamera();
  print('/////////// REPORT /////////////');

  // Builder
  var report = ReportBuilder()
      .addLighting('On')
      .addTransport('Busses on')
      .addSecurity('Cameras on')
      .addEnergy('Energy Save')
      .build();
  report.show();
  print('////////////////////////');

  // Facade OFF
  controller.shutDownCity();
}
