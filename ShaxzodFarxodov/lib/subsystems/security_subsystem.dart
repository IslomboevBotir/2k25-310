import 'dart:io';
import 'subsystem.dart';

/// Security subsystem - manages surveillance and alarm systems
class SecuritySubsystem implements ISubsystem {
  @override
  void initialize() {
    stdout.writeln(
      '🔐 Security Subsystem: Activating surveillance and alarm systems...',
    );
    stdout.writeln('   - 📹 Security cameras: Online');
    stdout.writeln('   - 🚨 Alarm system: Active');
  }

  @override
  void shutdown() {
    stdout.writeln('🔐 Security Subsystem: Surveillance offline');
  }

  @override
  void reportStatus() {
    stdout.writeln('   🔐 Security: 320 cameras online | Incidents: 0');
  }

  @override
  String getName() => 'Security Management';
}
