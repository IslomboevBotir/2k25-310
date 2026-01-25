/// Core interface for all subsystems
abstract class ISubsystem {
  /// Initialize the subsystem
  void initialize();

  /// Shutdown the subsystem
  void shutdown();

  /// Report current status
  void reportStatus();

  /// Get subsystem name
  String getName();
}
