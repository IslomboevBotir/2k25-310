// bin/main.dart
import 'dart:io';

import 'package:dart_application_1/core/controller.dart';

/// Console entrypoint for the SmartCity System.
/// Uses the SmartCityController singleton (which also acts as a Facade) to interact with subsystems.

Future<void> main() async {
  final controller = SmartCityController.instance; // Singleton + Facade
  print('--- SmartCity System (Dart) ---');

  while (true) {
    print('\nCommands: status | light:on | light:off | weather | transport:start | transport:stop | security:arm | security:disarm | exit');
    stdout.write('> ');
    final input = stdin.readLineSync()?.trim();
    if (input == null) continue;
    if (input == 'exit') break;

    switch (input) {
      case 'status':
        controller.printStatus();
        break;
      case 'light:on':
        controller.enableLighting();
        break;
      case 'light:off':
        controller.disableLighting();
        break;
      case 'weather':
        await controller.fetchWeatherAndAdjustLighting();
        break;
      case 'transport:start':
        controller.startTransport();
        break;
      case 'transport:stop':
        controller.stopTransport();
        break;
      case 'security:arm':
        controller.armSecurity();
        break;
      case 'security:disarm':
        controller.disarmSecurity();
        break;
      default:
        print('Unknown command');
    }
  }

  print('Bye!');
}
