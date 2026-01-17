import 'dart:io';
import 'dart:math';

abstract class Subsystem {
  String get name;
  String status();
  void performAction(String action);
}

class SubsystemFactory {
  Subsystem create(String type) {
    switch (type.toLowerCase()) {
      case 'lighting':
        return LightingSubsystem();
      case 'transport':
        return TransportSubsystem();
      case 'security':
        return SecuritySubsystem();
      case 'energy':
        return EnergySubsystem();
      case 'monitoring':
        return MonitoringSubsystem();
      default:
        throw ArgumentError('Unknown subsystem type: $type');
    }
  }
}

class SubsystemProxy implements Subsystem {
  final Subsystem _real;
  final String _requiredRole;

  SubsystemProxy(this._real, {String requiredRole = 'operator'})
    : _requiredRole = requiredRole;

  @override
  String get name => _real.name;

  bool _hasAccess(String role) {
    if (role == 'admin') return true;
    if (role == _requiredRole) return true;
    return false;
  }

  @override
  String status() {
    _log('status requested for ${_real.name}');
    return _real.status();
  }

  @override
  void performAction(String action) {
    final role = CityController.instance.currentRole;
    if (!_hasAccess(role)) {
      print(
        'Access denied for role "$role" to perform "$action" on ${_real.name}.',
      );
      return;
    }
    _log('performAction "$action" on ${_real.name} by role "$role"');
    _real.performAction(action);
  }

  void _log(String msg) {
    print('[Proxy Log] $msg');
  }
}

class ExternalWeatherService {
  String fetchCityWeather(String city) {
    final opts = ['Sunny', 'Rainy', 'Cloudy', 'Windy', 'Stormy'];
    final w = opts[Random().nextInt(opts.length)];
    return '{"city":"$city","condition":"$w","temp":${15 + Random().nextInt(15)}}';
  }
}

class WeatherAdapter {
  final ExternalWeatherService _service;
  WeatherAdapter(this._service);
  Map<String, dynamic> getWeather(String city) {
    final raw = _service.fetchCityWeather(city);
    // simple parsing simulation
    final condition =
        RegExp(r'"condition":"([^"]+)"').firstMatch(raw)?.group(1) ?? 'Unknown';
    final tempStr = RegExp(r'"temp":(\d+)').firstMatch(raw)?.group(1) ?? '0';
    return {'city': city, 'condition': condition, 'temp': int.parse(tempStr)};
  }
}

class LightingSubsystem implements Subsystem {
  bool _lightsOn = false;
  @override
  String get name => 'Lighting';

  @override
  String status() => _lightsOn ? 'Lights are ON' : 'Lights are OFF';

  @override
  void performAction(String action) {
    switch (action.toLowerCase()) {
      case 'toggle':
        _lightsOn = !_lightsOn;
        print('Lighting: toggled -> ${_lightsOn ? "ON" : "OFF"}');
        break;
      case 'status':
        print('Lighting status: ${status()}');
        break;
      default:
        print('Lighting: Unknown action "$action"');
    }
  }
}

class TransportSubsystem implements Subsystem {
  int _busesRunning = 5;
  @override
  String get name => 'Transport';

  @override
  String status() => 'Buses running: $_busesRunning';

  @override
  void performAction(String action) {
    switch (action.toLowerCase()) {
      case 'increase':
        _busesRunning++;
        print('Transport: increased buses -> $_busesRunning');
        break;
      case 'decrease':
        if (_busesRunning > 0) _busesRunning--;
        print('Transport: decreased buses -> $_busesRunning');
        break;
      case 'status':
        print('Transport status: ${status()}');
        break;
      default:
        print('Transport: Unknown action "$action"');
    }
  }
}

class SecuritySubsystem implements Subsystem {
  bool _alarmOn = false;
  @override
  String get name => 'Security';

  @override
  String status() => _alarmOn ? 'Alarm ON' : 'Alarm OFF';

  @override
  void performAction(String action) {
    switch (action.toLowerCase()) {
      case 'arm':
        _alarmOn = true;
        print('Security: armed');
        break;
      case 'disarm':
        _alarmOn = false;
        print('Security: disarmed');
        break;
      case 'status':
        print('Security status: ${status()}');
        break;
      default:
        print('Security: Unknown action "$action"');
    }
  }
}

class EnergySubsystem implements Subsystem {
  double _consumption = 1200.0; // kWh
  @override
  String get name => 'Energy';

  @override
  String status() => 'Consumption: ${_consumption.toStringAsFixed(1)} kWh';

  @override
  void performAction(String action) {
    switch (action.toLowerCase()) {
      case 'optimize':
        _consumption *= 0.95;
        print(
          'Energy: optimization applied. New consumption: ${_consumption.toStringAsFixed(1)} kWh',
        );
        break;
      case 'status':
        print('Energy status: ${status()}');
        break;
      default:
        print('Energy: Unknown action "$action"');
    }
  }
}

class MonitoringSubsystem implements Subsystem {
  final WeatherAdapter _weather;
  MonitoringSubsystem([WeatherAdapter? weather])
    : _weather = weather ?? WeatherAdapter(ExternalWeatherService());

  @override
  String get name => 'Monitoring';

  @override
  String status() {
    final w = _weather.getWeather('SmartCity');
    return 'Weather: ${w['condition']} ${w['temp']}°C';
  }

  @override
  void performAction(String action) {
    switch (action.toLowerCase()) {
      case 'check':
      case 'status':
        print('Monitoring status: ${status()}');
        break;
      default:
        print('Monitoring: Unknown action "$action"');
    }
  }
}

class City {
  final String name;
  final Map<String, Subsystem> subsystems;
  City(this.name, this.subsystems);
}

class CityBuilder {
  String _name = 'SmartCity';
  final Map<String, Subsystem> _subsystems = {};
  final SubsystemFactory _factory = SubsystemFactory();

  CityBuilder setName(String name) {
    _name = name;
    return this;
  }

  CityBuilder addSubsystem(
    String type, {
    bool viaProxy = true,
    String role = 'operator',
  }) {
    final s = _factory.create(type);
    if (viaProxy) {
      _subsystems[s.name.toLowerCase()] = SubsystemProxy(s, requiredRole: role);
    } else {
      _subsystems[s.name.toLowerCase()] = s;
    }
    return this;
  }

  City build() {
    return City(_name, Map.unmodifiable(_subsystems));
  }
}

class CityController {
  CityController._internal();
  static final CityController _instance = CityController._internal();
  static CityController get instance => _instance;

  late City _city;
  String currentRole = 'operator'; // role can be 'operator' or 'admin'

  void initialize(City city) {
    _city = city;
    print('CityController initialized for city: ${city.name}');
  }

  void listSubsystems() {
    print('\nAvailable subsystems:');
    _city.subsystems.forEach((k, v) {
      print('- ${v.name} (key: $k) -- ${v.status()}');
    });
  }

  void subsystemStatus(String key) {
    final sub = _city.subsystems[key.toLowerCase()];
    if (sub == null) {
      print('No subsystem with key "$key"');
      return;
    }
    print('${sub.name} -> ${sub.status()}');
  }

  void perform(String key, String action) {
    final sub = _city.subsystems[key.toLowerCase()];
    if (sub == null) {
      print('No subsystem with key "$key"');
      return;
    }
    sub.performAction(action);
  }

  void setRole(String role) {
    currentRole = role;
    print('Role set to "$role"');
  }

  Map<String, Subsystem> get subsystems => _city.subsystems;
}

void printHelp() {
  print('''
Commands:
  help                       - show this help
  list                       - list subsystems
  status <key>               - show status of subsystem (e.g., status lighting)
  action <key> <action>      - perform action on subsystem (e.g., action lighting toggle)
  role <admin|operator>      - change current role (affects proxy access)
  exit                       - quit
Notes:
  Subsystem keys: lighting, transport, security, energy, monitoring
  Actions vary per subsystem. Try 'status' first.
''');
}

void main() {
  final builder = CityBuilder()
      .setName('THiNKuz-SmartCity')
      .addSubsystem('lighting', viaProxy: true, role: 'operator')
      .addSubsystem('transport', viaProxy: true, role: 'operator')
      .addSubsystem(
        'security',
        viaProxy: true,
        role: 'admin',
      ) // security limited to admin
      .addSubsystem('energy', viaProxy: true, role: 'operator')
      .addSubsystem(
        'monitoring',
        viaProxy: false,
      ); // monitoring uses adapter internally

  final city = builder.build();

  final controller = CityController.instance;
  controller.initialize(city);

  print('Welcome to the SmartCity Console!');
  printHelp();

  while (true) {
    stdout.write('\n> ');
    final input = stdin.readLineSync();
    if (input == null) break;
    final parts = input.trim().split(RegExp(r'\s+'));
    if (parts.isEmpty) continue;
    final cmd = parts[0].toLowerCase();

    try {
      if (cmd == 'help') {
        printHelp();
      } else if (cmd == 'list') {
        controller.listSubsystems();
      } else if (cmd == 'status') {
        if (parts.length < 2) {
          print('Usage: status <key>');
        } else {
          controller.subsystemStatus(parts[1]);
        }
      } else if (cmd == 'action') {
        if (parts.length < 3) {
          print('Usage: action <key> <action>');
        } else {
          final key = parts[1];
          final action = parts.sublist(2).join(' ');
          controller.perform(key, action);
        }
      } else if (cmd == 'role') {
        if (parts.length < 2) {
          print('Usage: role <admin|operator>');
        } else {
          final role = parts[1].toLowerCase();
          if (role != 'admin' && role != 'operator') {
            print('Unknown role: $role');
          } else {
            controller.setRole(role);
          }
        }
      } else if (cmd == 'exit' || cmd == 'quit') {
        print('Bye!');
        break;
      } else {
        print('Unknown command: $cmd (type "help")');
      }
    } catch (e, st) {
      print('Error: $e\n$st');
    }
  }
}
