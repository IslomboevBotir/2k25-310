import 'dart:io';

/// PROXY PATTERN
/// Controls access to sensitive subsystems (Security)
/// Implements authorization before allowing access to real system

/// Security access interface
abstract class ISecurityAccess {
  void accessSecurityData();
  void setAlarm(bool active);
}

/// Real security system - actual implementation
class RealSecuritySystem implements ISecurityAccess {
  @override
  void accessSecurityData() {
    stdout.writeln('   📹 Accessing security camera feeds...');
  }

  @override
  void setAlarm(bool active) {
    stdout.writeln('   🚨 Alarm ${active ? "ACTIVATED" : "DEACTIVATED"}');
  }
}

/// Security proxy - controls access with authorization
class SecurityProxy implements ISecurityAccess {
  final RealSecuritySystem _realSystem = RealSecuritySystem();
  bool _isAuthorized;

  SecurityProxy({bool authorized = false}) : _isAuthorized = authorized;

  @override
  void accessSecurityData() {
    if (_isAuthorized) {
      _realSystem.accessSecurityData();
    } else {
      stdout.writeln('   ❌ ACCESS DENIED - Insufficient permissions');
    }
  }

  @override
  void setAlarm(bool active) {
    if (_isAuthorized) {
      _realSystem.setAlarm(active);
    } else {
      stdout.writeln('   ❌ ALARM CONTROL DENIED');
    }
  }

  /// Authorize access with password
  void authorizeAccess(String password) {
    if (password == 'admin123') {
      _isAuthorized = true;
      stdout.writeln('   ✓ Authorization granted');
    } else {
      stdout.writeln('   ❌ Invalid password');
    }
  }

  /// Check if currently authorized
  bool isAuthorized() => _isAuthorized;
}
