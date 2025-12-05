import '../core/factories.dart';

class TransportSystem {
  String run() => TransportFactory.createTransport('bus').move();
}


