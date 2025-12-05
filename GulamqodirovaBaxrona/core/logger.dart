class Logger {
  Logger._internal();
  static final Logger instance = Logger._internal();

  void log(String msg) {
    final time = DateTime.now().toIso8601String();
    print('[LOG] $time — $msg');
  }
}