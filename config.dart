class SystemConfig {
  static final SystemConfig _instance = SystemConfig._internal();
  
  String cityCode = "TASH-01";
  String operator = "Admin";
  bool isDebug = true;

  factory SystemConfig() {
    return _instance;
  }

  SystemConfig._internal();
}