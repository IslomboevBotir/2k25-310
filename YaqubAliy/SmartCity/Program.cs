using SmartCity.Core;

namespace SmartCity
{
    /// <summary>
    /// Main application entry point for SmartCity System
    /// Demonstrates the usage of multiple design patterns in a smart city management context
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║    SMART CITY MANAGEMENT SYSTEM        ║");
            Console.WriteLine("╔════════════════════════════════════════╗\n");

            // SINGLETON PATTERN: Get the single instance of CityController
            var cityController = CityController.Instance;
            cityController.InitializeCity();

            bool running = true;
            while (running)
            {
                Console.WriteLine("\n═══════════════════════════════════════");
                Console.WriteLine("MAIN MENU:");
                Console.WriteLine("1. Manage Street Lighting");
                Console.WriteLine("2. Manage Transportation");
                Console.WriteLine("3. Security System Status");
                Console.WriteLine("4. Energy Monitoring");
                Console.WriteLine("5. Emergency Alert");
                Console.WriteLine("6. City Status Report");
                Console.WriteLine("0. Exit");
                Console.WriteLine("═══════════════════════════════════════");
                Console.Write("Select option: ");

                string choice = Console.ReadLine() ?? "0";

                switch (choice)
                {
                    case "1":
                        ManageLighting(cityController);
                        break;
                    case "2":
                        ManageTransportation(cityController);
                        break;
                    case "3":
                        cityController.CheckSecurityStatus();
                        break;
                    case "4":
                        cityController.MonitorEnergy();
                        break;
                    case "5":
                        Console.Write("Enter emergency message: ");
                        string message = Console.ReadLine() ?? "Unknown emergency";
                        cityController.TriggerEmergency(message);
                        break;
                    case "6":
                        cityController.GenerateCityReport();
                        break;
                    case "0":
                        running = false;
                        Console.WriteLine("\n👋 Shutting down Smart City System...");
                        break;
                    default:
                        Console.WriteLine("❌ Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void ManageLighting(CityController controller)
        {
            Console.WriteLine("\n--- LIGHTING MANAGEMENT ---");
            Console.WriteLine("1. Turn ON all lights");
            Console.WriteLine("2. Turn OFF all lights");
            Console.WriteLine("3. Set brightness level");
            Console.Write("Select: ");

            string choice = Console.ReadLine() ?? "0";
            switch (choice)
            {
                case "1":
                    controller.ControlLighting("ON", 100);
                    break;
                case "2":
                    controller.ControlLighting("OFF", 0);
                    break;
                case "3":
                    Console.Write("Enter brightness (0-100): ");
                    if (int.TryParse(Console.ReadLine(), out int brightness))
                    {
                        controller.ControlLighting("DIM", brightness);
                    }
                    break;
            }
        }

        static void ManageTransportation(CityController controller)
        {
            Console.WriteLine("\n--- TRANSPORTATION MANAGEMENT ---");
            Console.WriteLine("1. Optimize traffic flow");
            Console.WriteLine("2. Emergency vehicle priority");
            Console.Write("Select: ");

            string choice = Console.ReadLine() ?? "0";
            switch (choice)
            {
                case "1":
                    controller.ManageTraffic("OPTIMIZE");
                    break;
                case "2":
                    controller.ManageTraffic("EMERGENCY");
                    break;
            }
        }
    }
}