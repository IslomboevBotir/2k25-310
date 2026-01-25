import 'dart:io';
import '../subsystems/subsystem.dart';

/// BUILDER PATTERN
/// Constructs complex subsystems step by step
/// Uses fluent interface for readable, chainable construction

/// Custom subsystem - result of builder
class CustomSubsystem implements ISubsystem {
  final String _name;
  final int _capacity;
  final bool _autoMode;
  final List<String> _components;

  CustomSubsystem(this._name, this._capacity, this._autoMode, this._components);

  @override
  void initialize() {
    stdout.writeln('⚙️ ${_name} initialized with capacity ${_capacity}');
  }

  @override
  void shutdown() {
    stdout.writeln('⏸️ ${_name} shutdown complete');
  }

  @override
  void reportStatus() {
    final components = _components.isEmpty ? 'None' : _components.join(', ');
    stdout.writeln(
      '   ${_name}: Active | Auto: ${_autoMode ? "Yes" : "No"} | Components: $components',
    );
  }

  @override
  String getName() => _name;
}

/// Subsystem builder - constructs subsystems step by step
class SubsystemBuilder {
  String _name = 'Unnamed';
  int _capacity = 0;
  bool _autoMode = false;
  final List<String> _components = [];

  /// Set subsystem name
  SubsystemBuilder setName(String name) {
    _name = name;
    return this;
  }

  /// Set subsystem capacity
  SubsystemBuilder setCapacity(int capacity) {
    _capacity = capacity;
    return this;
  }

  /// Enable automatic mode
  SubsystemBuilder enableAutoMode() {
    _autoMode = true;
    return this;
  }

  /// Add a component to the subsystem
  SubsystemBuilder addComponent(String component) {
    _components.add(component);
    return this;
  }

  /// Build and return the subsystem
  CustomSubsystem build() {
    return CustomSubsystem(_name, _capacity, _autoMode, List.from(_components));
  }
}
