import 'dart:math';

/// WeatherAdapter implements the Adapter pattern to adapt an external
/// weather service to our internal usage. Here it's a small mocked adapter.
class WeatherAdapter {
  /// In a real app this would call an HTTP API; here we simulate variability.
  Future<String> getCurrentCondition() async {
    await Future.delayed(Duration(milliseconds: 150));
    final values = ['clear', 'rain', 'clouds'];
    return values[Random().nextInt(values.length)];
  }
}
