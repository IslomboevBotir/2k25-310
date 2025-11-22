import '../../core/factories/module_factory.dart';

class SecurityModule implements Module {
  @override
  void status() {
    print('Security: cameras active, doors locked');
  }

  bool grantAccess() {
    print('Security: access granted to secure resource');
    return true;
  }
}
