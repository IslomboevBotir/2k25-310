import 'core/controller.dart';

void main() {
  final controller = CentralController.instance;

  print('Running basic tests...');

  controller.toggleLighting();
  assert(controller.lightsOn != null);

  controller.requestTransportUpdate();

  controller.requestSecureResource('guest');
  controller.requestSecureResource('admin');

  controller.buildAndPrintReport();

  print('If no assertion failed and output looks reasonable, tests passed.');
}
