import '../../modules/security/security_module.dart';

class SecurityProxy {
  final SecurityModule _real;
  SecurityProxy(this._real);

  bool requestAccess(String role) {
    if (role == 'admin') {
      return _real.grantAccess();
    } else {
      // guests can only read status
      _real.status();
      return false;
    }
  }
}
