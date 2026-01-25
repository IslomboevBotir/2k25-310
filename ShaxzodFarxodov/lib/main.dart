import 'dart:io';
import 'package:dart_application_1/factories/abstract_factory.dart';

import 'controller.dart';
import 'facade.dart';
import 'patterns/decorator.dart';
import 'patterns/proxy.dart';
import 'patterns/builder.dart';

void main() async {
  stdout.writeln('═══════════════════════════════════════');
  stdout.writeln('   🏙️ SMARTCITY SYSTEM INITIALIZATION 🏙️');
  stdout.writeln('═══════════════════════════════════════\n');

  // Facade pattern - Simple interface
  final facade = SmartCityFacade();

  await facade.initializeSmartCity();
  await Future.delayed(Duration(milliseconds: 500));

  facade.start();
  await Future.delayed(Duration(milliseconds: 500));

  facade.showStatus();
  await Future.delayed(Duration(milliseconds: 500));

  stdout.writeln('🧪 TESTING ADVANCED PATTERNS:\n');

  testDecorator();
  await Future.delayed(Duration(milliseconds: 300));

  stdout.writeln('');
  testProxy();
  await Future.delayed(Duration(milliseconds: 300));

  stdout.writeln('');
  testBuilder();
  await Future.delayed(Duration(milliseconds: 300));

  stdout.writeln('\n📋 INTERACTIVE MENU\n');
  await showInteractiveMenu();

  stdout.writeln('\n🛑 Shutting down SmartCity System...');
  facade.stop();
}

void testDecorator() {
  stdout.writeln('📝 DECORATOR PATTERN TEST:');

  final lamp = SmartLamp();
  stdout.writeln('   Base: ${lamp.getDeviceInfo()}');

  final wifiLamp = WiFiEnabledDecorator(lamp);
  stdout.writeln('   + WiFi: ${wifiLamp.getDeviceInfo()}');

  final aiLamp = AIEnabledDecorator(wifiLamp);
  stdout.writeln('   + AI: ${aiLamp.getDeviceInfo()}');
  stdout.writeln('   Features: ${aiLamp.getFeatures().join(", ")}\n');
}

void testProxy() {
  stdout.writeln('📝 PROXY PATTERN TEST:');
  stdout.writeln('   Scenario 1: Unauthorized access');

  final proxy = SecurityProxy(authorized: false);
  proxy.accessSecurityData();
  proxy.setAlarm(true);

  stdout.writeln('\n   Scenario 2: Authorized access');
  proxy.authorizeAccess('admin123');
  proxy.accessSecurityData();
  proxy.setAlarm(true);
}

void testBuilder() {
  stdout.writeln('📝 BUILDER PATTERN TEST:');

  final custom = SubsystemBuilder()
      .setName('Water Management')
      .setCapacity(5000)
      .enableAutoMode()
      .addComponent('Pump')
      .addComponent('Valve')
      .addComponent('Filter')
      .build();

  custom.initialize();
  custom.reportStatus();
}

Future<void> showInteractiveMenu() async {
  final controller = CityController.instance;
  bool running = true;

  while (running) {
    stdout.writeln('Select option:');
    stdout.writeln('  1. Show System Status');
    stdout.writeln('  2. Test Decorator Pattern');
    stdout.writeln('  3. Test Proxy Pattern');
    stdout.writeln('  4. Test Builder Pattern');
    stdout.writeln('  5. Exit');
    stdout.write('> ');

    String? input = stdin.readLineSync();

    switch (input) {
      case '1':
        controller.displayStatus();
        break;
      case '2':
        testDecorator();
        break;
      case '3':
        testProxy();
        break;
      case '4':
        testBuilder();
        break;
      case '5':
        running = false;
        break;
      default:
        stdout.writeln('❌ Invalid option\n');
    }
  }
}
