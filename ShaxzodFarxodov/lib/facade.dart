import 'dart:io';
import 'controller.dart';
import 'factories/subsystem_factory.dart';

/// FACADE PATTERN
/// Provides simplified interface to complex subsystem interactions
/// Hides complexity of creating and managing multiple subsystems
class SmartCityFacade {
  final CityController _controller = CityController.instance;

  /// Initialize all subsystems using factories
  Future<void> initializeSmartCity() async {
    stdout.writeln('═══════════════════════════════════════');
    stdout.writeln('   🏙️ INITIALIZING SMART CITY SYSTEM 🏙️');
    stdout.writeln('═══════════════════════════════════════\n');

    // FACTORY METHOD PATTERN: Create subsystems without specifying exact classes
    final lighting = SubsystemFactory.createSubsystem(SubsystemType.lighting);
    final transport = SubsystemFactory.createSubsystem(SubsystemType.transport);
    final security = SubsystemFactory.createSubsystem(SubsystemType.security);
    final energy = SubsystemFactory.createSubsystem(SubsystemType.energy);

    _controller.registerSubsystem(lighting);
    _controller.registerSubsystem(transport);
    _controller.registerSubsystem(security);
    _controller.registerSubsystem(energy);
  }

  /// Start the smart city system
  void start() => _controller.startSystem();

  /// Stop the smart city system
  void stop() => _controller.stopSystem();

  /// Display system status
  void showStatus() => _controller.displayStatus();
}
