class CityConfig {
  static final CityConfig _instance = CityConfig._internal();
  String cityName;
  bool isDayTime;

  CityConfig._internal() : cityName = "Smart Tashkent", isDayTime = true;

  factory CityConfig() {
    return _instance;
  }

  void setTime(bool isDay) {
    isDayTime = isDay;
    print("Vaqt o'zgardi: ${isDay ? "Kunduz" : "Tun"}");
  }
}