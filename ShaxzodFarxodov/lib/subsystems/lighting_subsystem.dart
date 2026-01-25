import 'dart:io';
import 'subsystem.dart';
import '../factories/abstract_factory.dart';

/// Lighting subsystem - manages street lights and indoor lighting
class LightingSubsystem implements ISubsystem {
  late ISubsystemFactory _factory;

  LightingSubsystem() {
    _factory = LightingFactory();
  }

  @override
  void initialize() {
    stdout.writeln(
      '🔌 Lighting Subsystem: Initializing street lights and indoor lighting...',
    );

    // ABSTRACT FACTORY PATTERN: Create related device and sensor families
    final device = _factory.createDevice();
    final sensor = _factory.createSensor();

    stdout.writeln('   - ${device.getDeviceInfo()}');
    stdout.writeln('   - ${sensor.readData()}');
  }

  @override
  void shutdown() {
    stdout.writeln('🔌 Lighting Subsystem: All lights turned off');
  }

  @override
  void reportStatus() {
    stdout.writeln('   🔌 Lighting: 450/500 devices active | Power: 45kW');
  }

  @override
  String getName() => 'Lighting Management';
}
