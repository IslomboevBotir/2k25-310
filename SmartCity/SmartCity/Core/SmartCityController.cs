using SmartCitySystem.Core.Adapters;
using SmartCitySystem.Core.Factories;
using SmartCitySystem.Modules.Lighting;
using SmartCitySystem.Modules.Transport;

namespace SmartCitySystem.Core
{
    // Singleton: Faqat bitta nusxani ta'minlaydi.
    // Facade: Kichik tizimlar bilan ishlashni soddalashtiradi.
    public sealed class SmartCityController
    {
        // Singleton Implementation
        private static readonly Lazy<SmartCityController> _lazyInstance =
            new Lazy<SmartCityController>(() => new SmartCityController());

        public static SmartCityController Instance => _lazyInstance.Value;

        // Kichik tizimlar
        private readonly TrafficManager _trafficManager;
        private readonly StreetLightManager _lightManager;
        // Dizayn naqshlari komponentlari
        private readonly AbstractFactory _currentFactory;
        private readonly ICityWeather _weatherSource;

        // Private Constructor (Singleton uchun zarur)
        private SmartCityController()
        {
            _trafficManager = new TrafficManager();
            _lightManager = new StreetLightManager();

            // Abstract Factory initsializatsiyasi
            _currentFactory = new AdvancedFactory();

            // Adapter initsializatsiyasi: Tashqi xizmatni ichki interfeysga o'rab olamiz
            _weatherSource = new ThirdPartyWeatherAdapter(new ThirdPartyWeatherService());
        }

        // Facade Metodi 1: Asosiy optimallashtirish (Bir nechta tizimni birlashtiradi)
        public void OptimizeCityOperation()
        {
            Console.WriteLine("\n--- SmartCity Facade: Shahar Operatsiyalarini Optimallashtirish ---");
            _trafficManager.AdjustTrafficFlow();
            _lightManager.OptimizeLightingBasedOnTraffic();

            if (_weatherSource.IsRaining()) // Adapter orqali olingan ma'lumot
            {
                Console.WriteLine("Ob-havo: Yomg'ir. Qo'shimcha xavfsizlik va yoritish rejimini faollashtirish.");
            }
            Console.WriteLine("--- Optimizatsiya yakunlandi ---");
        }

        // Facade Metodi 2: Abstract Factory orqali qurilma statusini ko'rsatish
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

        // Facade Metodi 3: Adapter orqali ob-havo ma'lumotlarini olish
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