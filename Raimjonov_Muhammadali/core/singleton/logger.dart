class Logger {
  Logger._internal();
  static final Logger _instance = Logger._internal();
  static Logger get instance => _instance;

  void log(String message) {
    final ts = DateTime.now().toIso8601String();
    print('[LOG] \$ts - \$message');
  }
}
