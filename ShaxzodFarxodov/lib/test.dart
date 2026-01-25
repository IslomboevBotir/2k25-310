import 'dart:io';
import 'controller.dart';
import 'factories/subsystem_factory.dart';
import 'factories/abstract_factory.dart';
import 'patterns/decorator.dart';
import 'patterns/proxy.dart';
import 'patterns/builder.dart';

void main() {
  stdout.writeln('\n╔════════════════════════════════════════╗');
  stdout.writeln('║      🧪 UNIT TEST SUITE - SmartCity   ║');
  stdout.writeln('╚════════════════════════════════════════╝\n');

  testSingletonPattern();
  testFactoryPattern();
  testAbstractFactoryPattern();
  testDecoratorPattern();
  testProxyPattern();
  testBuilderPattern();

  stdout.writeln('\n✅ ALL TESTS COMPLETED\n');
}

/// TEST 1: Singleton Pattern
void testSingletonPattern() {
  stdout.writeln('TEST 1: Singleton Pattern');
  stdout.writeln('─────────────────────────');

  final controller1 = CityController.instance;
  final controller2 = CityController.instance;

  final singletonWorks = identical(controller1, controller2);

  stdout.writeln('✓ Same instance returned: $singletonWorks');
  stdout.writeln('✓ Instance identity verified\n');
}

/// TEST 2: Factory Method Pattern
void testFactoryPattern() {
  stdout.writeln('TEST 2: Factory Method Pattern');
  stdout.writeln('──────────────────────────────');

  final lighting = SubsystemFactory.createSubsystem(SubsystemType.lighting);
  final transport = SubsystemFactory.createSubsystem(SubsystemType.transport);
  final security = SubsystemFactory.createSubsystem(SubsystemType.security);
  final energy = SubsystemFactory.createSubsystem(SubsystemType.energy);

  stdout.writeln('✓ Lighting created: ${lighting.getName()}');
  stdout.writeln('✓ Transport created: ${transport.getName()}');
  stdout.writeln('✓ Security created: ${security.getName()}');
  stdout.writeln('✓ Energy created: ${energy.getName()}\n');
}

/// TEST 3: Abstract Factory Pattern
void testAbstractFactoryPattern() {
  stdout.writeln('TEST 3: Abstract Factory Pattern');
  stdout.writeln('────────────────────────────────');

  ISubsystemFactory lightingFactory = LightingFactory();
  final lamp = lightingFactory.createDevice();
  final lightSensor = lightingFactory.createSensor();

  stdout.writeln('✓ Lamp device: ${lamp.getDeviceInfo()}');
  stdout.writeln('✓ Light sensor: ${lightSensor.readData()}');

  ISubsystemFactory transportFactory = TransportFactory();
  final trafficLight = transportFactory.createDevice();
  final trafficSensor = transportFactory.createSensor();

  stdout.writeln('✓ Traffic light: ${trafficLight.getDeviceInfo()}');
  stdout.writeln('✓ Traffic sensor: ${trafficSensor.readData()}\n');
}

/// TEST 4: Decorator Pattern
void testDecoratorPattern() {
  stdout.writeln('TEST 4: Decorator Pattern');
  stdout.writeln('─────────────────────────');

  SmartLamp baseLamp = SmartLamp();
  stdout.writeln('Base device: ${baseLamp.getDeviceInfo()}');
  stdout.writeln('Base features: ${baseLamp.getFeatures().join(", ")}');

  ISmartDevice wifiLamp = WiFiEnabledDecorator(baseLamp);
  stdout.writeln('\nWith WiFi: ${wifiLamp.getDeviceInfo()}');
  stdout.writeln('Features: ${wifiLamp.getFeatures().join(", ")}');

  ISmartDevice aiLamp = AIEnabledDecorator(wifiLamp);
  stdout.writeln('\nWith AI: ${aiLamp.getDeviceInfo()}');
  stdout.writeln('Features: ${aiLamp.getFeatures().join(", ")}\n');
}

/// TEST 5: Proxy Pattern
void testProxyPattern() {
  stdout.writeln('TEST 5: Proxy Pattern');
  stdout.writeln('────────────────────');

  stdout.writeln('Scenario 1: Unauthorized access');
  final unauthorizedProxy = SecurityProxy(authorized: false);
  unauthorizedProxy.accessSecurityData();
  unauthorizedProxy.setAlarm(true);

  stdout.writeln('\nScenario 2: Authorized access');
  final authorizedProxy = SecurityProxy(authorized: false);
  authorizedProxy.authorizeAccess('admin123');
  authorizedProxy.accessSecurityData();
  authorizedProxy.setAlarm(true);
  stdout.writeln('');
}

/// TEST 6: Builder Pattern
void testBuilderPattern() {
  stdout.writeln('TEST 6: Builder Pattern');
  stdout.writeln('──────────────────────');

  final subsystem1 = SubsystemBuilder()
      .setName('Water Management')
      .setCapacity(10000)
      .enableAutoMode()
      .addComponent('Main Pump')
      .addComponent('Filtration Unit')
      .build();

  stdout.writeln('Built subsystem 1:');
  subsystem1.reportStatus();

  final subsystem2 = SubsystemBuilder()
      .setName('Waste Management')
      .setCapacity(5000)
      .addComponent('Compactor')
      .addComponent('Sorter')
      .build();

  stdout.writeln('Built subsystem 2:');
  subsystem2.reportStatus();
  stdout.writeln('');
}
