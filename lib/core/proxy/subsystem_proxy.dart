// lib/core/proxy/subsystem_proxy.dart

import '../subsystem.dart';

/// SubsystemProxy implements the Proxy pattern by controlling access to a subsystem.
/// It can perform authorization checks, lazy initialization, and logging.
class SubsystemProxy implements Subsystem {
  final Subsystem _real;
  bool _authorized = true; // For demo purposes; could be configurable

  SubsystemProxy(this._real);

  @override
  void start() {
    if (!_authorized) {
      print('Access denied to start subsystem');
      return;
    }
    print('[Proxy] Logging: start called');
    _real.start();
  }

  @override
  void stop() {
    if (!_authorized) {
      print('Access denied to stop subsystem');
      return;
    }
    print('[Proxy] Logging: stop called');
    _real.stop();
  }

  @override
  String status() => _real.status();
}
