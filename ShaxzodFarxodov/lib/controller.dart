import 'dart:io';
import 'subsystems/subsystem.dart';

/// SINGLETON PATTERN
/// Ensures only one instance of CityController exists globally
/// Controls all subsystems and manages system lifecycle
class CityController {
  static final CityController _instance = CityController._internal();

  static CityController get instance => _instance;

  final List<ISubsystem> _subsystems = [];
  bool _systemRunning = false;

  CityController._internal();

  /// Register a subsystem to the controller
  void registerSubsystem(ISubsystem subsystem) {
    _subsystems.add(subsystem);
    stdout.writeln('✓ ${subsystem.getName()} registered');
  }

  /// Start all registered subsystems
  void startSystem() {
    _systemRunning = true;
    stdout.writeln('\n🏙️ SmartCity System ACTIVATED\n');

    for (var subsystem in _subsystems) {
      subsystem.initialize();
    }
  }

  /// Stop all registered subsystems
  void stopSystem() {
    _systemRunning = false;
    stdout.writeln('\n🛑 SmartCity System DEACTIVATED\n');

    for (var subsystem in _subsystems) {
      subsystem.shutdown();
    }
  }

  /// Display status of all subsystems
  void displayStatus() {
    stdout.writeln('\n📊 SYSTEM STATUS:\n');

    for (var subsystem in _subsystems) {
      subsystem.reportStatus();
    }

    stdout.writeln('');
  }

  /// Get subsystem by name
  ISubsystem? getSubsystem(String name) {
    try {
      return _subsystems.firstWhere((s) => s.getName().contains(name));
    } catch (e) {
      return null;
    }
  }

  /// Get all subsystems
  List<ISubsystem> getAllSubsystems() => List.unmodifiable(_subsystems);

  /// Check if system is running
  bool isRunning() => _systemRunning;
}
