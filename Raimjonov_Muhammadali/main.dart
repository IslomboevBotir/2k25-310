import 'dart:io';

import 'core/controller.dart';

void main() {
  final controller = CentralController.instance;

  print('=== SmartCity System (simple) ===');

  while (true) {
    print('\nChoose action:');
    print('1) Show status');
    print('2) Toggle lighting');
    print('3) Request transport update');
    print('4) Security: access resource (with proxy)');
    print('5) Generate report');
    print('0) Exit');

    final input = stdin.readLineSync();
    if (input == null) continue;

    switch (input.trim()) {
      case '1':
        controller.showStatus();
        break;
      case '2':
        controller.toggleLighting();
        break;
      case '3':
        controller.requestTransportUpdate();
        break;
      case '4':
        print('Enter user role (guest/admin): ');
        final role = stdin.readLineSync() ?? 'guest';
        controller.requestSecureResource(role.trim());
        break;
      case '5':
        controller.buildAndPrintReport();
        break;
      case '0':
        print('Goodbye.');
        return;
      default:
        print('Unknown option');
    }
  }
}
