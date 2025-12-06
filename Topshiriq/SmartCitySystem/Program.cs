using SmartCitySystem.Core;
using SmartCitySystem.Factories;
using SmartCitySystem.Adapters;
using SmartCitySystem.Proxy;
using SmartCitySystem.Energy;
using SmartCitySystem.Patterns;
Console.WriteLine("=== SmartCity System - Lab Work #1 ===\n");

var controller = CityController.Instance;

controller.Transport.AddVehicle(new BusFactory(), "Bus-001");
controller.Transport.AddVehicle(new TramFactory(), "Tram-05");


var weather = new WeatherAdapter(new ExternalWeatherService());
Console.WriteLine($"[Adapter] Hozirgi ob-havo: {weather.GetCurrentWeather()}");

var config = new SignalConfig { GreenSeconds = 40, RedSeconds = 20 };
var cloned = (SignalConfig)config.Clone();
Console.WriteLine($"[Prototype] Klonlandi: Yashil={cloned.GreenSeconds}s");


var userProxy = new SecurityProxy(controller.Security, "user");
var adminProxy = new SecurityProxy(controller.Security, "admin");

userProxy.Arm();     
adminProxy.Arm();   


var report = controller.Energy.BuildReport(new MonthlyEnergyReportBuilder());
Console.WriteLine($"[Builder] {report}");

controller.PrintStatus();

while (true)
{
    Console.WriteLine("\n=== MENU ===");
    Console.WriteLine("1) Kechki rejim");
    Console.WriteLine("2) Favqulodda holat");
    Console.WriteLine("3) Transportni ishga tushirish");
    Console.WriteLine("4) Holatni ko‘rish");
    Console.WriteLine("5) Chiqish");
    Console.Write("Tanlang: ");

    switch (Console.ReadLine())
    {
        case "1": controller.EveningMode(); break;
        case "2": controller.EmergencyMode(); break;
        case "3": controller.Transport.OperateAll(); break;
        case "4": controller.PrintStatus(); break;
        case "5": return;
        default: Console.WriteLine("Noto‘g‘ri buyruq!"); break;
    }
}