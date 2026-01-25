import '../subsystems/subsystem.dart';
import '../subsystems/lighting_subsystem.dart';
import '../subsystems/transport_subsystem.dart';
import '../subsystems/security_subsystem.dart';
import '../subsystems/energy_subsystem.dart';

/// Subsystem types enum
enum SubsystemType { lighting, transport, security, energy }

/// FACTORY METHOD PATTERN
/// Creates subsystems without specifying exact classes
/// Abstracts the creation process and allows easy extension
class SubsystemFactory {
  /// Factory method - creates appropriate subsystem based on type
  static ISubsystem createSubsystem(SubsystemType type) {
    switch (type) {
      case SubsystemType.lighting:
        return LightingSubsystem();
      case SubsystemType.transport:
        return TransportSubsystem();
      case SubsystemType.security:
        return SecuritySubsystem();
      case SubsystemType.energy:
        return EnergySubsystem();
    }
  }
}
