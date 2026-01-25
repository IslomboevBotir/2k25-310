import 'dart:io';
import 'subsystem.dart';
import '../factories/abstract_factory.dart';

/// Transport subsystem - manages traffic lights and vehicle flow
class TransportSubsystem implements ISubsystem {
  late ISubsystemFactory _factory;

  TransportSubsystem() {
    _factory = TransportFactory();
  }

  @override
  void initialize() {
    stdout.writeln(
      '🚗 Transport Subsystem: Initializing traffic management system...',
    );

    // ABSTRACT FACTORY PATTERN: Create related device and sensor families
    final device = _factory.createDevice();
    final sensor = _factory.createSensor();

    stdout.writeln('   - ${device.getDeviceInfo()}');
    stdout.writeln('   - ${sensor.readData()}');
  }

  @override
  void shutdown() {
    stdout.writeln('🚗 Transport Subsystem: Traffic system offline');
  }

  @override
  void reportStatus() {
    stdout.writeln('   🚗 Transport: 120 intersections | Avg flow: 85%');
  }

  @override
  String getName() => 'Transport Management';
}
