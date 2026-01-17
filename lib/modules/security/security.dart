// lib/modules/security/security.dart

import '../../core/subsystem.dart';

/// Security subsystem (simple demo)
class SecuritySubsystem implements Subsystem {
  bool _armed = false;

  @override
  void start() {
    _armed = true;
    print('Security subsystem armed.');
  }

  @override
  void stop() {
    _armed = false;
    print('Security subsystem disarmed.');
  }

  @override
  String status() => 'Security: ${_armed ? 'ARMED' : 'DISARMED'}';
}
