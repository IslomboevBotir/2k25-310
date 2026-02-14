using SmartCitySystem.Core;
using SmartCitySystem.Core.Factories;

namespace SmartCitySystem.Modules.Lighting;

/// <summary>
/// Lighting Management Subsystem
/// Manages all street lights and public lighting in the city
/// </summary>
public class LightingSubsystem : ISubsystem
{
    private readonly List<IInfrastructureDevice> _lights;
    private bool _isRunning;
    private int _brightness;

    public string Name => "Lighting Management System";

    public LightingSubsystem()
    {
        _lights = new List<IInfrastructureDevice>();
        _brightness = 100;
        _isRunning = false;
        
        // Initialize with some default lights
        var factory = new LightingDeviceFactory();
        for (int i = 1; i <= 5; i++)
        {
            _lights.Add(factory.CreateDevice($"LIGHT-{i:D3}"));
        }
    }

    public void Start()
    {
        _isRunning = true;
        Console.WriteLine($"{Name} started.");
    }

    public void Stop()
    {
        _isRunning = false;
        foreach (var light in _lights)
        {
            light.Deactivate();
        }
        Console.WriteLine($"{Name} stopped.");
    }

    public string GetStatus()
    {
        var activeCount = _lights.Count(l => 
        {
            var info = l.GetInfo();
            return info.Contains("ON");
        });
        
        return $"Status: {(_isRunning ? "Running" : "Stopped")}\n" +
               $"Total Lights: {_lights.Count}\n" +
               $"Active Lights: {activeCount}\n" +
               $"Brightness Level: {_brightness}%";
    }

    /// <summary>
    /// Turns on all street lights
    /// </summary>
    public void TurnOnAllLights()
    {
        foreach (var light in _lights)
        {
            light.Activate();
        }
        Console.WriteLine("All lights turned on.");
    }

    /// <summary>
    /// Turns off all street lights
    /// </summary>
    public void TurnOffAllLights()
    {
        foreach (var light in _lights)
        {
            light.Deactivate();
        }
        Console.WriteLine("All lights turned off.");
    }

    /// <summary>
    /// Sets the brightness level for all lights
    /// </summary>
    public void SetBrightness(int brightness)
    {
        if (brightness < 0 || brightness > 100)
        {
            Console.WriteLine("Brightness must be between 0 and 100.");
            return;
        }
        
        _brightness = brightness;
        Console.WriteLine($"Brightness set to {_brightness}%");
    }

    /// <summary>
    /// Displays all lights status
    /// </summary>
    public void DisplayLightsStatus()
    {
        Console.WriteLine("\n--- Lighting Status ---");
        foreach (var light in _lights)
        {
            Console.WriteLine(light.GetInfo());
        }
        Console.WriteLine("-----------------------\n");
    }

    /// <summary>
    /// Adds a new light to the system
    /// </summary>
    public void AddLight(IInfrastructureDevice light)
    {
        _lights.Add(light);
        Console.WriteLine($"Added light: {light.Id}");
    }
}
