import 'dart:io';
import 'package:main/core/adapter.dart';
import 'package:main/core/config.dart';
import 'package:main/core/facade.dart';
import 'package:main/core/factory.dart';

void main() {
  final dashboard = CityControlPanel();
  final config = SystemConfig(); 
  
  bool isActive = true;

  print("Tizim yuklandi... Shahar: ${config.cityCode}");

  while (isActive) {
    print('Menyu:');
    print('1. Tungi rejim');
    print('2. Kunduzgi rejim');
    print('3. Diagnostika');
    print('4. Harorat');
    print('5. Hisobot');
    print('6. Favqulodda Xolat');
    print('0. Chiqish');
    
    stdout.write('Menu dan birini tanlang: ');
    String? input = stdin.readLineSync();

    switch (input) {
      case '1':
        dashboard.activateNightMode();
        break;
      case '2':
        dashboard.activateDayMode();
        break;
      case '3':
        stdout.write('Qurilma turi: ');
        var type = stdin.readLineSync();
        try {
          if (type != null && type.isNotEmpty) {
            var dev = DeviceFactory.getDevice(type);
            dev.testConnection();
          }
        } catch (e) {
          print("Xato: Bunday qurilma bazada yo'q.");
        }
        print('');
        break;
      case '4':
        var oldDev = OldThermoSensor();
        var adapter = TempAdapter(oldDev);
        print("\nSensor ma'lumoti: ${adapter.getCelsius().toStringAsFixed(1)}°C (Norma)\n");
        break;
      case '5':
        var report = ReportBuilder()
            .initReport()
            .addStatus("Lighting", "Active")
            .addStatus("Security", "Standby")
            .signOff()
            .build();
        print('\n$report\n');
        break;
      case '6':
        dashboard.emergencyAlert();
        break;
      case '0':
        isActive = false;
        print("Tizim o'chirildi");
        break;
      default:
        print("Noto'g'ri buyruq");
    }
  }
}