import 'package:test/test.dart';
import 'package:main/core/singleton/city_config.dart';
import 'package:main/core/factories/sensor_factory.dart';
import 'package:main/core/proxy/security_proxy.dart';

void main() {
  group("SmartCity System Tests", () {
    
    test("Singleton: CityConfig har doim bitta nusxada bo'lishi kerak", () {
      var config1 = CityConfig();
      var config2 = CityConfig();
      
      config1.cityName = "New Tashkent";

      expect(config1, same(config2));
      expect(config2.cityName, equals("New Tashkent"));
    });

    test("Factory: To'g'ri turdagi sensorlarni yaratishi kerak", () {
      var sensor1 = SensorFactory.createSensor('traffic');
      var sensor2 = SensorFactory.createSensor('env');

      expect(sensor1, isA<TrafficSensor>());
      expect(sensor2, isA<EnvironmentSensor>());
    });

    test("Factory: Noma'lum sensor turi uchun xatolik otishi kerak", () {
      expect(() => SensorFactory.createSensor('unknown'), throwsException);
    });

    test("Proxy: Noto'g'ri parol bilan kirishni bloklashi kerak", () {
      
      var proxy = SecurityProxy("wrong_pass");
      expect(proxy.authenticate(), isFalse);
    });

    test("Proxy: To'g'ri parol bilan kirishga ruxsat berishi kerak", () {
      var proxy = SecurityProxy("admin123");
      expect(proxy.authenticate(), isTrue);
    });
  });
}