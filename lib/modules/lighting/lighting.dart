// lib/modules/lighting/lighting.dart

import '../../core/subsystem.dart';

/// Lighting subsystem
/// - Demonstrates the Composite pattern via Device and DeviceGroup
/// - Also provides a simple setScene method (could be built with Builder pattern for complex scenes)

abstract class Device {
  void on();
  void off();
  String getStatus();
}

class StreetLight implements Device {
  final String id;
  bool _on = false;
  StreetLight(this.id);

  @override
  void on() => _on = true;

  @override
  void off() => _on = false;

  @override
  String getStatus() => 'StreetLight($id): ${_on ? 'ON' : 'OFF'}';
}

class DeviceGroup implements Device {
  final String name;
  final List<Device> _children = [];
  DeviceGroup(this.name);
  void add(Device d) => _children.add(d);

  @override
  void on() => _children.forEach((c) => c.on());

  @override
  void off() => _children.forEach((c) => c.off());

  @override
  String getStatus() => '${name} -> [${_children.map((c) => c.getStatus()).join(', ')}]';
}

class LightingSubsystem implements Subsystem {
  final DeviceGroup _allLights = DeviceGroup('AllLights');

  LightingSubsystem() {
    // create some lights (demonstration of Composite leaf and group)
    for (var i = 1; i <= 3; i++) {
      _allLights.add(StreetLight('L\$i'));
    }
  }

  /// Set a simple scene by name. In a fuller solution we'd implement Builder for complex scenes.
  void setScene(String scene) {
    if (scene == 'high') {
      _allLights.on();
    } else if (scene == 'normal') {
      _allLights.off();
    }
  }

  @override
  void start() {
    _allLights.on();
    print('Lighting subsystem started');
  }

  @override
  void stop() {
    _allLights.off();
    print('Lighting subsystem stopped');
  }

  @override
  String status() => _allLights.getStatus();
}
