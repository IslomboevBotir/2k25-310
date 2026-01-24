class OldThermoSensor {
  double getTempF() => 89.6; 
}

class TempAdapter {
  final OldThermoSensor _oldSensor;

  TempAdapter(this._oldSensor);

  double getCelsius() {
    var f = _oldSensor.getTempF();
    return (f - 32) * 5 / 9;
  }
}

class DailyReport {
  String _header = '';
  String _body = '';
  String _footer = '';

  void setHeader(String h) => _header = h;
  void appendData(String d) => _body += "$d\n";
  void setFooter(String f) => _footer = f;

  @override
  String toString() => '$_header\n$_body$_footer';
}

class ReportBuilder {
  final DailyReport _report = DailyReport();

  ReportBuilder initReport() {
    _report.setHeader("SMART CITY KUNLIK HISOBOT");
    return this;
  }

  ReportBuilder addStatus(String subsystem, String status) {
    _report.appendData("$subsystem: $status");
    return this;
  }

  ReportBuilder signOff() {
    _report.setFooter("Tizim Administratori\nVaqt: ${DateTime.now().toString().split('.')[0]}");
    return this;
  }

  DailyReport build() => _report;
}