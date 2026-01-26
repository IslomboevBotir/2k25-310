abstract class ISecuritySystem {
  void activateLockdown();
}

class CentralSecurityHub implements ISecuritySystem {
  @override
  void activateLockdown() {
    print("XAVFSIZLIK: SHAHAR YOPILDI! Barcha eshiklar bloklandi.");
  }
}

class SecurityProxy implements ISecuritySystem {
  final CentralSecurityHub _realHub = CentralSecurityHub();
  final String _password;

  SecurityProxy(this._password);

  @override
  void activateLockdown() {
    if (authenticate()) {
      _realHub.activateLockdown();
    } else {
      print("Xatolik: Xavfsizlik tizimiga kirish rad etildi!");
    }
  }

  bool authenticate() {
    return _password == "admin123";
  }
}