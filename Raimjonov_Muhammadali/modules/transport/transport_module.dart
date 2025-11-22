import '../../core/factories/module_factory.dart';

class TransportModule implements Module {
  String _status = 'ok';

  @override
  void status() {
    print('Transport status: \$_status');
  }

  void update(String weather) {
    if (weather == 'rain') {
      _status = 'delays expected';
    } else {
      _status = 'running normally';
    }
    print('Transport updated for weather: \$weather');
  }
}
