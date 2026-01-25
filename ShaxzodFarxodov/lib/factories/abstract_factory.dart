/// Device interface - represents smart city devices
abstract class IDevice {
  String getDeviceInfo();
  void operate();
}

/// Sensor interface - represents sensing devices
abstract class ISensor {
  String readData();
}

/// ABSTRACT FACTORY PATTERN
/// Creates families of related devices and sensors
/// Ensures consistency between related objects
abstract class ISubsystemFactory {
  IDevice createDevice();
  ISensor createSensor();
}

// ===== LIGHTING FAMILY =====

/// Smart lamp device
class SmartLamp implements IDevice, ISmartDevice {
  bool _isOn = false;
  final List<String> _features = ['Brightness Control', 'Color Adjustment'];

  @override
  String getDeviceInfo() => '💡 Smart Lamp (LED, 60W)';

  @override
  void operate() {
    _isOn = !_isOn;
  }

  @override
  void addFeature(String feature) {
    _features.add(feature);
  }

  @override
  List<String> getFeatures() => List.unmodifiable(_features);
}

/// Light sensor
class LightSensor implements ISensor {
  @override
  String readData() => '💡 Light Level: 850 lux';
}

/// Lighting factory - creates lamp and sensor
class LightingFactory implements ISubsystemFactory {
  @override
  IDevice createDevice() => SmartLamp();

  @override
  ISensor createSensor() => LightSensor();
}

// ===== TRANSPORT FAMILY =====

/// Traffic light device
class TrafficLight implements IDevice, ISmartDevice {
  final List<String> _features = ['Motion Detection', 'Schedule Control'];

  @override
  String getDeviceInfo() => '🚦 Traffic Light (Adaptive)';

  @override
  void operate() {}

  @override
  void addFeature(String feature) {
    _features.add(feature);
  }

  @override
  List<String> getFeatures() => List.unmodifiable(_features);
}

/// Traffic sensor
class TrafficSensor implements ISensor {
  @override
  String readData() => '🚗 Vehicle Count: 42/min';
}

/// Transport factory - creates traffic light and sensor
class TransportFactory implements ISubsystemFactory {
  @override
  IDevice createDevice() => TrafficLight();

  @override
  ISensor createSensor() => TrafficSensor();
}

// ===== SMART DEVICE INTERFACE =====

/// Extended device interface for decorator pattern
abstract class ISmartDevice implements IDevice {
  void addFeature(String feature);
  List<String> getFeatures();
}
