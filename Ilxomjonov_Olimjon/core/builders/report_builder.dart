class CityReport {
  String report = "";
  void show() => print(report);
}

class ReportBuilder {
  final CityReport _report = CityReport();

  ReportBuilder addLighting(String status) {
    _report.report += "Lighting: $status \n";
    return this;
  }

  ReportBuilder addTransport(String status) {
    _report.report += "Transport: $status \n";
    return this;
  }

  ReportBuilder addEnergy(String status) {
    _report.report += "Energy: $status\n";
    return this;
  }

  ReportBuilder addSecurity(String status) {
    _report.report += "Security: $status\n";
    return this;
  }

  CityReport build() => _report;
}
