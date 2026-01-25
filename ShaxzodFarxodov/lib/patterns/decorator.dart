import '../factories/abstract_factory.dart';

/// DECORATOR PATTERN
/// Adds functionality to devices dynamically at runtime
/// Allows feature composition without modifying original classes
abstract class SmartDeviceDecorator implements ISmartDevice {
  final ISmartDevice _device;
  final List<String> _features;

  SmartDeviceDecorator(this._device)
    : _features = List.from(_device.getFeatures());

  @override
  String getDeviceInfo() => _device.getDeviceInfo();

  @override
  void operate() => _device.operate();

  @override
  void addFeature(String feature) {
    _features.add(feature);
  }

  @override
  List<String> getFeatures() => List.unmodifiable(_features);
}

/// WiFi enabled decorator - adds connectivity features
class WiFiEnabledDecorator extends SmartDeviceDecorator {
  WiFiEnabledDecorator(ISmartDevice device) : super(device) {
    addFeature('WiFi Connectivity');
  }

  @override
  String getDeviceInfo() => '${_device.getDeviceInfo()} [WiFi: ON]';
}

/// AI enabled decorator - adds analytics features
class AIEnabledDecorator extends SmartDeviceDecorator {
  AIEnabledDecorator(ISmartDevice device) : super(device) {
    addFeature('AI Analytics');
  }

  @override
  String getDeviceInfo() => '${_device.getDeviceInfo()} [AI: ON]';
}

/// Voice control decorator - adds voice features
class VoiceControlDecorator extends SmartDeviceDecorator {
  VoiceControlDecorator(ISmartDevice device) : super(device) {
    addFeature('Voice Control');
  }

  @override
  String getDeviceInfo() => '${_device.getDeviceInfo()} [Voice: ON]';
}
