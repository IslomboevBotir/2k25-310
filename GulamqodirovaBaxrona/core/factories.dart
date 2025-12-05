abstract class Transport {
  String move();
}

class Bus implements Transport {
  @override
  String move() => 'Bus is moving along the route';
}

class Taxi implements Transport {
  @override
  String move() => 'Taxi is transporting a passenger';
}

class TransportFactory {
  static Transport createTransport(String kind) {
    switch (kind) {
      case 'bus':
        return Bus();
      case 'taxi':
        return Taxi();
      default:
        throw ArgumentError('Unknown transport type: \$kind');
    }
  }
}



