class CityReportBuilder {
  String _title = '';
  final List<String> _sections = [];
  String _footer = '';

  CityReportBuilder setTitle(String title) {
    _title = title;
    return this;
  }

  CityReportBuilder addSection(String name, String body) {
    _sections.add('== \$name ==\n\$body');
    return this;
  }

  CityReportBuilder setFooter(String footer) {
    _footer = footer;
    return this;
  }

  String build() {
    final buffer = StringBuffer();
    buffer.writeln('### \$_title ###');
    for (final s in _sections) {
      buffer.writeln(s + '\n');
    }
    buffer.writeln('--');
    buffer.writeln(_footer);
    return buffer.toString();
  }
}
