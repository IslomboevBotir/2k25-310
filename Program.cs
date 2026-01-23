using System;
using System.Collections.Generic;
using SmartCity.Adapters;
using SmartCity.Builders;
using SmartCity.Core;
using SmartCity.Factories;
using SmartCity.Modules;
using SmartCity.Proxies;

namespace SmartCity;

internal static class Program
{
    private static void Main()
    {
        Console.WriteLine("SmartCity Management Console");
        Console.Write("Enter role (Admin/Operator/Guest): ");
        var roleInput = Console.ReadLine() ?? "Guest";

        var config = new SmartCityConfigurationBuilder()
            .WithCityName("NeoMetro")
            .WithInitialLightingMode(LightingMode.Auto)
            .WithInitialRoutes(new List<string> { "Downtown Loop", "Airport Express" })
            .WithEnergyAlertThresholdKw(750)
            .WithSecurityArmed(false)
            .WithAllowedSecurityRoles(new List<string> { "Admin", "Operator" })
            .Build();

        var userContext = new UserContext(roleInput);
        var accessPolicy = new RoleBasedAccessPolicy(config.AllowedSecurityRoles);

        ISubsystemFactory factory = new SmartCitySubsystemFactory(
            new ExternalEnergyServiceAdapter(new LegacyEnergyService()),
            accessPolicy,
            userContext);

        var controller = SmartCityController.Instance;
        controller.Initialize(config, factory);

        var menu = new Dictionary<string, Action>
        {
            ["1"] = () => ManageLighting(controller),
            ["2"] = () => MonitorEnergy(controller),
            ["3"] = () => ManageTransport(controller),
            ["4"] = () => ManageSecurity(controller),
            ["5"] = () => ShowSystemStatus(controller),
        };

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("1) Control Lighting");
            Console.WriteLine("2) Monitor Energy Usage");
            Console.WriteLine("3) Manage Transport Routes");
            Console.WriteLine("4) Access Security (Proxy)");
            Console.WriteLine("5) System Status");
            Console.WriteLine("0) Exit");
            Console.Write("Select: ");
            var choice = Console.ReadLine() ?? string.Empty;

            if (choice == "0")
            {
                Console.WriteLine("Goodbye.");
                return;
            }

            if (menu.TryGetValue(choice, out var action))
            {
                action();
            }
            else
            {
                Console.WriteLine("Invalid selection.");
            }
        }
    }

    private static void ManageLighting(SmartCityController controller)
    {
        Console.WriteLine("Lighting Modes: 1) Off 2) On 3) Auto");
        Console.Write("Select: ");
        var choice = Console.ReadLine();

        var mode = choice switch
        {
            "1" => LightingMode.Off,
            "2" => LightingMode.On,
            _ => LightingMode.Auto
        };

        controller.SetLightingMode(mode);
        Console.WriteLine(controller.GetLightingStatus());
    }

    private static void MonitorEnergy(SmartCityController controller)
    {
        Console.WriteLine(controller.GetEnergyReport());
    }

    private static void ManageTransport(SmartCityController controller)
    {
        Console.WriteLine("1) Add Route 2) Remove Route 3) List Routes");
        Console.Write("Select: ");
        var choice = Console.ReadLine();

        if (choice == "1")
        {
            Console.Write("Route name: ");
            var route = Console.ReadLine() ?? string.Empty;
            controller.AddTransportRoute(route);
        }
        else if (choice == "2")
        {
            Console.Write("Route name: ");
            var route = Console.ReadLine() ?? string.Empty;
            controller.RemoveTransportRoute(route);
        }

        Console.WriteLine(controller.GetTransportStatus());
    }

    private static void ManageSecurity(SmartCityController controller)
    {
        Console.WriteLine("1) Arm 2) Disarm 3) Status");
        Console.Write("Select: ");
        var choice = Console.ReadLine();

        try
        {
            if (choice == "1")
            {
                controller.ArmSecurity();
            }
            else if (choice == "2")
            {
                controller.DisarmSecurity();
            }

            Console.WriteLine(controller.GetSecurityStatus());
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine($"Access denied: {ex.Message}");
        }
    }

    private static void ShowSystemStatus(SmartCityController controller)
    {
        Console.WriteLine(controller.GetLightingStatus());
        Console.WriteLine(controller.GetTransportStatus());
        Console.WriteLine(controller.GetEnergyReport());
        Console.WriteLine(controller.GetSecurityStatus());
    }
}