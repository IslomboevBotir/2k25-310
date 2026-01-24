import 'package:flutter_test/flutter_test.dart';
import 'package:smart_city_midterm/core/facade.dart';
import 'package:smart_city_midterm/core/logger.dart';
import 'package:smart_city_midterm/modules/energy.dart';
import '../2k25-310/FIO/builders/camera_builder.dart';
import '../2k25-310/FIO/core/factories.dart';

void main() {

  // 1. Singleton Test (Logger)
  test('Logger should be singleton', () {
    final a = Logger.instance;
    final b = Logger.instance;
    expect(a, same(b));
  });

  // 2. Factory Test - Bus
  test('TransportFactory creates Bus', () {
    final t = TransportFactory.createTransport('bus');
    expect(t.move(), 'Bus is moving along the route');
  });

  // 3. Factory Test - Taxi
  test('TransportFactory creates Taxi', () {
    final t = TransportFactory.createTransport('taxi');
    expect(t.move(), 'Taxi is transporting a passenger');
  });

  // 4. Builder Test
  test('CameraBuilder builds full-featured camera', () {
    final cam = CameraBuilder()
        .setResolution('4K')
        .enableNightVision()
        .enableMotionSensor()
        .build();

    expect(cam.resolution, '4K');
    expect(cam.nightVision, true);
    expect(cam.motionSensor, true);
  });

  // 5. Proxy Test - Correct Password
  test('EnergyProxy allows access with correct password', () {
    final energy = EnergyProxy('admin');
    expect(energy.getUsage('admin'), contains('Energy usage'));
  });

  // 6. Proxy Test - Wrong Password
  test('EnergyProxy blocks access with wrong password', () {
    final energy = EnergyProxy('admin');
    expect(energy.getUsage('1234'), 'Access denied');
  });

  // 7. Facade Test - Morning Mode
  test('SmartCityFacade morning mode works', () {
    final city = SmartCityFacade();
    final result = city.morningMode();

    expect(result.length, 3);
    expect(result[0], 'Street lights OFF');
    expect(result[1], 'Bus is moving along the route');
    expect(result[2], 'Security cameras scanning...');
  });

  // 8. Facade Test - Night Mode
  test('SmartCityFacade night mode works', () {
    final city = SmartCityFacade();
    final result = city.nightMode('admin');

    expect(result.length, 3);
    expect(result[0], 'Street lights ON');
    expect(result[1], 'Security cameras scanning...');
    expect(result[2], contains('Energy usage'));
  });
}
