using SmartCitySystem.Core;
using SmartCitySystem.Core.Builders;
using SmartCitySystem.Core.Singleton;

namespace SmartCitySystem;

/// <summary>
/// Main entry point for the Smart City Management System
/// Demonstrates all implemented design patterns:
/// 1. Singleton - SmartCityController
/// 2. Factory Method - InfrastructureFactory
/// 3. Builder - CityConfigurationBuilder
/// 4. Facade - SmartCityFacade
/// 5. Proxy - SecuritySubsystemProxy
/// 6. Adapter - WeatherServiceAdapter, TrafficServiceAdapter
/// </summary>
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("╔══════════════════════════════════════════╗");
        Console.WriteLine("║   Smart City Management System v1.0     ║");
        Console.WriteLine("╚══════════════════════════════════════════╝\n");

        // Demonstrate Builder Pattern - Create city configuration
        Console.WriteLine("--- Initializing City Configuration (Builder Pattern) ---");
        var builder = new CityConfigurationBuilder();
        var director = new CityDirector(builder);
        var cityConfig = director.BuildLargeCity("Smart Metropolis");
        cityConfig.DisplayConfiguration();

        // Use Facade Pattern to initialize the system
        var smartCity = new SmartCityFacade();
        smartCity.InitializeCity();

        // Main menu loop
        bool running = true;
        while (running)
        {
            DisplayMenu();
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    smartCity.DisplaySystemStatus();
                    break;
                case "2":
                    LightingControlMenu(smartCity);
                    break;
                case "3":
                    TransportationMenu(smartCity);
                    break;
                case "4":
                    SecurityMenu(smartCity);
                    break;
                case "5":
                    EnergyMenu(smartCity);
                    break;
                case "6":
                    ConfigurationMenu();
                    break;
                case "7":
                    DemonstrateSingletonPattern();
                    break;
                case "8":
                    smartCity.ShutdownCity();
                    running = false;
                    Console.WriteLine("\nThank you for using Smart City Management System!");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            if (running)
            {
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }

    static void DisplayMenu()
    {
        Console.WriteLine("\n╔════════════════════════════════════════════╗");
        Console.WriteLine("║   Smart City Management System - Menu     ║");
        Console.WriteLine("╚════════════════════════════════════════════╝");
        Console.WriteLine("1. View All Systems Status");
        Console.WriteLine("2. Lighting Control");
        Console.WriteLine("3. Transportation Management");
        Console.WriteLine("4. Security System");
        Console.WriteLine("5. Energy Monitoring");
        Console.WriteLine("6. System Configuration");
        Console.WriteLine("7. Demonstrate Singleton Pattern");
        Console.WriteLine("8. Exit");
        Console.WriteLine("─────────────────────────────────────────────");
        Console.Write("Enter your choice: ");
    }

    static void LightingControlMenu(SmartCityFacade smartCity)
    {
        Console.WriteLine("\n--- Lighting Control ---");
        Console.WriteLine("1. Turn On All Lights");
        Console.WriteLine("2. Turn Off All Lights");
        Console.WriteLine("3. Adjust Brightness");
        Console.WriteLine("4. View Lighting Status");
        Console.Write("Enter choice: ");
        
        var choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                smartCity.ControlLighting(true);
                break;
            case "2":
                smartCity.ControlLighting(false);
                break;
            case "3":
                Console.Write("Enter brightness (0-100): ");
                if (int.TryParse(Console.ReadLine(), out int brightness))
                {
                    smartCity.AdjustLightingBrightness(brightness);
                }
                break;
            case "4":
                smartCity.GetLightingSystem().DisplayLightsStatus();
                break;
        }
    }

    static void TransportationMenu(SmartCityFacade smartCity)
    {
        Console.WriteLine("\n--- Transportation Management ---");
        Console.WriteLine("1. Monitor Traffic Status (uses Adapter Pattern)");
        Console.WriteLine("2. Display Traffic Lights");
        Console.WriteLine("3. Optimize Traffic Flow");
        Console.Write("Enter choice: ");
        
        var choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                smartCity.MonitorTransportation();
                break;
            case "2":
                smartCity.GetTransportSystem().DisplayTrafficLights();
                break;
            case "3":
                smartCity.GetTransportSystem().OptimizeTrafficFlow();
                break;
        }
    }

    static void SecurityMenu(SmartCityFacade smartCity)
    {
        Console.WriteLine("\n--- Security System (uses Proxy Pattern) ---");
        Console.WriteLine("1. Check Security Alerts");
        Console.WriteLine("2. Access Security Camera");
        Console.WriteLine("3. View All Cameras");
        Console.WriteLine("4. Display Access Log");
        Console.Write("Enter choice: ");
        
        var choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                smartCity.CheckSecurityAlerts();
                break;
            case "2":
                Console.Write("Enter user ID (admin/operator/guest): ");
                var userId = Console.ReadLine() ?? "guest";
                Console.Write("Enter camera ID (CAM-001 to CAM-006): ");
                var cameraId = Console.ReadLine() ?? "CAM-001";
                smartCity.AccessSecurityCamera(userId, cameraId);
                break;
            case "3":
                smartCity.GetSecuritySystem().DisplayCamerasStatus();
                break;
            case "4":
                smartCity.GetSecuritySystem().DisplayAccessLog();
                break;
        }
    }

    static void EnergyMenu(SmartCityFacade smartCity)
    {
        Console.WriteLine("\n--- Energy Monitoring ---");
        Console.WriteLine("1. Display Energy Consumption");
        Console.WriteLine("2. Optimize Energy Usage");
        Console.WriteLine("3. Display Weather Data (uses Adapter Pattern)");
        Console.Write("Enter choice: ");
        
        var choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                smartCity.MonitorEnergy();
                break;
            case "2":
                smartCity.OptimizeEnergy();
                break;
            case "3":
                smartCity.GetEnergySystem().DisplayWeatherData();
                break;
        }
    }

    static void ConfigurationMenu()
    {
        Console.WriteLine("\n--- System Configuration (Builder Pattern Demo) ---");
        Console.WriteLine("Creating different city configurations...\n");
        
        var builder = new CityConfigurationBuilder();
        var director = new CityDirector(builder);
        
        Console.WriteLine("Small City Configuration:");
        var smallCity = director.BuildSmallCity("Small Town");
        smallCity.DisplayConfiguration();
        
        Console.WriteLine("Large City Configuration:");
        var largeCity = director.BuildLargeCity("Metropolis");
        largeCity.DisplayConfiguration();
    }

    static void DemonstrateSingletonPattern()
    {
        Console.WriteLine("\n--- Singleton Pattern Demonstration ---");
        Console.WriteLine("Getting controller instance 1...");
        var controller1 = SmartCityController.Instance;
        Console.WriteLine($"Controller 1 HashCode: {controller1.GetHashCode()}");
        
        Console.WriteLine("\nGetting controller instance 2...");
        var controller2 = SmartCityController.Instance;
        Console.WriteLine($"Controller 2 HashCode: {controller2.GetHashCode()}");
        
        Console.WriteLine($"\nAre they the same instance? {ReferenceEquals(controller1, controller2)}");
        Console.WriteLine("✓ Singleton pattern ensures only one instance exists!");
        Console.WriteLine("---------------------------------------");
    }
}
