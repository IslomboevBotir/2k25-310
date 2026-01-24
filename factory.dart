abstract class IoTDevice {
  void testConnection();
}

class StreetLamp implements IoTDevice {
  @override
  void testConnection() => print('Aloqa bor. Signal 98%');
}

class TrafficSensor implements IoTDevice {
  @override
  void testConnection() => print("Ma'lumotlar uzatilmoqda...");
}

class DeviceFactory {
  static IoTDevice getDevice(String type) {
    switch (type.toLowerCase()) {
      case 'lamp':
        return StreetLamp();
      case 'sensor':
        return TrafficSensor();
      default:
        throw Exception("Noma'lum qurilma turi");
    }
  }
}