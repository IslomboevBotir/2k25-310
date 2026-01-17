using SmartCitySystem.Core.Adapters;
using SmartCitySystem.Core.Factories;
using SmartCitySystem.Modules.Lighting;
using SmartCitySystem.Modules.Transport;

namespace SmartCitySystem.Core
{
    public sealed class SmartCityController
    {
        private static readonly Lazy<SmartCityController> _lazyInstance =
            new Lazy<SmartCityController>(() => new SmartCityController());

        public static SmartCityController Instance => _lazyInstance.Value;
        private readonly TrafficManager _trafficManager;
        private readonly StreetLightManager _lightManager;
        private readonly AbstractFactory _currentFactory;
        private readonly ICityWeather _weatherSource;
        private SmartCityController()
        {
            _trafficManager = new TrafficManager();
            _lightManager = new StreetLightManager();

            _currentFactory = new AdvancedFactory();

            _weatherSource = new ThirdPartyWeatherAdapter(new ThirdPartyWeatherService());
        }

        public void OptimizeCityOperation()
        {
            Console.WriteLine("\n--- SmartCity Facade: Shahar Operatsiyalarini Optimallashtirish ---");
            _trafficManager.AdjustTrafficFlow();
            _lightManager.OptimizeLightingBasedOnTraffic();

            if (_weatherSource.IsRaining())
            {
                Console.WriteLine("Ob-havo: Yomg'ir. Qo'shimcha xavfsizlik va yoritish rejimini faollashtirish.");
            }
            Console.WriteLine("--- Optimizatsiya yakunlandi ---");
        }
        public void DisplaySensorStatus()
        {
            Console.WriteLine("\n--- SmartCity Facade: Sensorlar Statusi ---");
            Console.WriteLine($"Ishlatilayotgan Factory: {_currentFactory.GetType().Name}");

            var sensor = _currentFactory.CreateTrafficSensor();
            var light = _currentFactory.CreateLightBulb();
            var camera = _currentFactory.CreateSecurityCamera();

            Console.WriteLine($"- Sensor: {sensor.GetStatus()}");
            Console.WriteLine($"- Kamera: {camera.GetResolution()} / {camera.GetType().Name}");
            light.TurnOn();
        }
        public void UpdateWeatherInfo()
        {
            Console.WriteLine("\n--- SmartCity Facade: Ob-havo Ma'lumotlarini Yangilash (Adapter ishladi) ---");
            double tempC = _weatherSource.GetCurrentTemperatureC();
            bool isRaining = _weatherSource.IsRaining();
            Console.WriteLine($"Shahardagi joriy harorat: {tempC:F2}°C.");
            Console.WriteLine($"Yog'ingarchilik: {(isRaining ? "Ha" : "Yo'q")}.");
        }
    }
}