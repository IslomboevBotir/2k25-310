abstract class Sensor {
  String collectData();
}

class TrafficSensor implements Sensor {
  @override
  String collectData() => "Tirbandlik darajasi: O'rtacha";
}

class EnvironmentSensor implements Sensor {
  @override
  String collectData() => "Havo tozaligi: 85 AQI";
}

class SensorFactory {
  static Sensor createSensor(String type) {
    if (type == 'traffic') {
      return TrafficSensor();
    } else if (type == 'env') {
      return EnvironmentSensor();
    } else {
      throw Exception("Noma'lum sensor turi");
    }
  }
}