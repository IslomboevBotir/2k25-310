import 'core/facade.dart';
import 'core/logger.dart';

void main() {
  final logger = Logger.instance;
  final city = SmartCityFacade();

  logger.log('Switching to morning mode');
  final morning = city.morningMode();
  for (final s in morning) {
    print(s);
  }

  print('');
  logger.log('Switching to night mode');
  final night = city.nightMode('admin');
  for (final s in night) {
    print(s);
  }
}
