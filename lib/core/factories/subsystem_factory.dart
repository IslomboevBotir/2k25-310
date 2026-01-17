// lib/core/factories/subsystem_factory.dart
import '../../modules/lighting/lighting.dart';
import '../../modules/security/security.dart';
import '../../modules/transport/transport.dart';

/// Abstract factory interface to create subsystems.
/// Using **Abstract Factory** allows producing families of related subsystem objects.
abstract class SubsystemFactory {
  LightingSubsystem createLightingSubsystem();
  TransportSubsystem createTransportSubsystem();
  SecuritySubsystem createSecuritySubsystem();
}

/// Default concrete factory producing default subsystem implementations.
class DefaultSubsystemFactory implements SubsystemFactory {
  @override
  LightingSubsystem createLightingSubsystem() => LightingSubsystem();

  @override
  TransportSubsystem createTransportSubsystem() => TransportSubsystem();

  @override
  SecuritySubsystem createSecuritySubsystem() => SecuritySubsystem();
}
