class EnergySystem {
  String getUsage() => 'Energy usage: 145 kW';
}

class EnergyProxy {
  final EnergySystem _real = EnergySystem();
  final String _password;

  EnergyProxy(this._password);

  String getUsage(String pwd) {
    if (pwd != _password) return 'Access denied';
    return _real.getUsage();
  }
}