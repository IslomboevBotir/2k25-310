abstract class IEnergySource {
  void supplyPower();
}

class OldDieselGenerator {
  void startGenerator() {
    print("Eski dizel generatori ishga tushdi");
  }
}

class LegacyEnergyAdapter implements IEnergySource {
  final OldDieselGenerator _legacyGenerator;

  LegacyEnergyAdapter(this._legacyGenerator);

  @override
  void supplyPower() {
    print("Adapter ishga tushdi");
    _legacyGenerator.startGenerator();
  }
}