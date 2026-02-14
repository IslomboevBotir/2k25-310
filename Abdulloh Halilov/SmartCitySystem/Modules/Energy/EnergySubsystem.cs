using SmartCitySystem.Core;
using SmartCitySystem.Core.Adapters;
using SmartCitySystem.Core.Factories;

namespace SmartCitySystem.Modules.Energy;

/// <summary>
/// Energy Management Subsystem
/// Monitors energy consumption and optimizes usage
/// </summary>
public class EnergySubsystem : ISubsystem
{
    private readonly List<IInfrastructureDevice> _meters;
    private readonly IWeatherProvider _weatherProvider;
    private bool _isRunning;
    private double _currentConsumption;
    private bool _isOptimized;

    public string Name => "Energy Monitoring & Management System";

    public EnergySubsystem()
    {
        _meters = new List<IInfrastructureDevice>();
        _isRunning = false;
        _currentConsumption = 0;
        _isOptimized = false;
        
        // Initialize with energy meters
        var factory = new EnergyDeviceFactory();
        for (int i = 1; i <= 3; i++)
        {
            _meters.Add(factory.CreateDevice($"EM-{i:D3}"));
        }

        // Use Adapter pattern to integrate external weather service
        var legacyWeatherService = new LegacyWeatherService();
        _weatherProvider = new WeatherServiceAdapter(legacyWeatherService);
    }

    public void Start()
    {
        _isRunning = true;
        foreach (var meter in _meters)
        {
            meter.Activate();
        }
        // Simulate initial consumption
        _currentConsumption = CalculateConsumption();
        Console.WriteLine($"{Name} started.");
    }

    public void Stop()
    {
        _isRunning = false;
        foreach (var meter in _meters)
        {
            meter.Deactivate();
        }
        Console.WriteLine($"{Name} stopped.");
    }

    public string GetStatus()
    {
        var activeCount = _meters.Count(m => 
        {
            var info = m.GetInfo();
            return info.Contains("Monitoring");
        });
        
        return $"Status: {(_isRunning ? "Running" : "Stopped")}\n" +
               $"Energy Meters: {_meters.Count}\n" +
               $"Active Meters: {activeCount}\n" +
               $"Current Consumption: {_currentConsumption:F2} kWh\n" +
               $"Optimization: {(_isOptimized ? "Enabled" : "Disabled")}";
    }

    /// <summary>
    /// Displays current energy consumption
    /// </summary>
    public void DisplayEnergyConsumption()
    {
        Console.WriteLine("\n--- Energy Consumption ---");
        Console.WriteLine($"Total Consumption: {_currentConsumption:F2} kWh");
        Console.WriteLine($"Average per Meter: {(_currentConsumption / _meters.Count):F2} kWh");
        
        // Display individual meters
        foreach (var meter in _meters)
        {
            Console.WriteLine(meter.GetInfo());
        }
        
        // Show weather impact using Adapter
        try
        {
            var weather = _weatherProvider.GetWeatherData();
            Console.WriteLine($"\nCurrent Weather: {weather}");
            Console.WriteLine("Weather-based energy adjustments applied.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Weather data unavailable: {ex.Message}");
        }
        
        Console.WriteLine("--------------------------\n");
    }

    /// <summary>
    /// Optimizes energy consumption
    /// </summary>
    public void OptimizeConsumption()
    {
        Console.WriteLine("\n--- Energy Optimization ---");
        Console.WriteLine("Analyzing consumption patterns...");
        
        if (!_isOptimized)
        {
            // Reduce consumption by 15%
            _currentConsumption *= 0.85;
            _isOptimized = true;
            Console.WriteLine("✓ Optimization enabled");
            Console.WriteLine($"✓ Consumption reduced to {_currentConsumption:F2} kWh");
            Console.WriteLine("✓ Energy-saving modes activated");
        }
        else
        {
            Console.WriteLine("System is already optimized.");
        }
        
        Console.WriteLine("---------------------------\n");
    }

    /// <summary>
    /// Calculates current energy consumption
    /// </summary>
    private double CalculateConsumption()
    {
        // Simulate consumption based on number of devices
        Random random = new Random();
        return _meters.Count * (50 + random.NextDouble() * 50); // 50-100 kWh per meter
    }

    /// <summary>
    /// Updates consumption values
    /// </summary>
    public void UpdateConsumption()
    {
        if (_isRunning)
        {
            _currentConsumption = CalculateConsumption();
            if (_isOptimized)
            {
                _currentConsumption *= 0.85;
            }
        }
    }

    /// <summary>
    /// Adds a new energy meter
    /// </summary>
    public void AddMeter(IInfrastructureDevice meter)
    {
        _meters.Add(meter);
        Console.WriteLine($"Added energy meter: {meter.Id}");
        UpdateConsumption();
    }

    /// <summary>
    /// Gets current weather data
    /// </summary>
    public void DisplayWeatherData()
    {
        Console.WriteLine("\n--- Weather Information ---");
        try
        {
            var weather = _weatherProvider.GetWeatherData();
            Console.WriteLine(weather.ToString());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        Console.WriteLine("---------------------------\n");
    }
}
