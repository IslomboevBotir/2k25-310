using System;
using Factories;
using Core;
using Adapters;
using Builders;
using Proxy;
using Decorators;
using Modules;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== SmartCity System (C# Lab №1) ===\n");

        var controller = CentralController.Instance;

        var facade = new SmartCityFacade(controller);

        var weather = new ExternalWeatherAdapter();

        var factory = new SubsystemFactory();

        var lighting = factory.CreateSubsystem("Lighting");
        var transport = factory.CreateSubsystem("Transport");
        var security = factory.CreateSubsystem("Security");
        var energy = factory.CreateSubsystem("Energy");

        var lightingProxy = new SubsystemProxy(lighting, "guest"); 
        var transportProxy = new SubsystemProxy(transport, "admin");

        var loggingSecurity = new LoggingDecorator(security);

        facade.RegisterSubsystem("lighting", lightingProxy);
        facade.RegisterSubsystem("transport", transportProxy);
        facade.RegisterSubsystem("security", loggingSecurity);
        facade.RegisterSubsystem("energy", energy);

        while (true)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1) Show status");
            Console.WriteLine("2) Start all subsystems");
            Console.WriteLine("3) Stop all subsystems");
            Console.WriteLine("4) Toggle lighting (via proxy)");shu
            Console.WriteLine("5) Get weather (adapter)");
            Console.WriteLine("6) Build system report (builder)");
            Console.WriteLine("0) Exit");
            Console.Write("Choice: ");
            var key = Console.ReadLine();

            switch (key)
            {
                case "1":
                    facade.ShowStatus();
                    break;
                case "2":
                    facade.StartAll();
                    break;
                case "3":
                    facade.StopAll();
                    break;
                case "4":
                    facade.ExecuteOnSubsystem("lighting", s => s.Toggle());
                    break;
                case "5":
                    Console.WriteLine("Weather: " + weather.GetCurrentWeather());
                    break;
                case "6":
                    var rb = new ReportBuilder();
                    string report = rb
                        .StartReport("SmartCity daily report")
                        .AddSection("Status", facade.GetStatusSummary())
                        .AddSection("Weather", weather.GetCurrentWeather())
                        .Finish();
                    Console.WriteLine("\n--- REPORT ---\n" + report);
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Unknown option.");
                    break;
            }
        }
    }
}
