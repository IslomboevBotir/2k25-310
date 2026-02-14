using SmartCitySystem.Core;
using SmartCitySystem.Core.Adapters;
using SmartCitySystem.Core.Factories;

namespace SmartCitySystem.Modules.Transport;

/// <summary>
/// Transportation Management Subsystem
/// Manages traffic lights, monitors traffic flow, and public transportation
/// </summary>
public class TransportSubsystem : ISubsystem
{
    private readonly List<IInfrastructureDevice> _trafficLights;
    private readonly ITrafficDataProvider _trafficDataProvider;
    private bool _isRunning;
    private string _currentTrafficStatus;

    public string Name => "Transportation Management System";

    public TransportSubsystem()
    {
        _trafficLights = new List<IInfrastructureDevice>();
        _isRunning = false;
        _currentTrafficStatus = "Normal";
        
        // Initialize with traffic lights
        var factory = new TransportDeviceFactory();
        for (int i = 1; i <= 4; i++)
        {
            _trafficLights.Add(factory.CreateDevice($"TL-{i:D3}"));
        }

        // Use Adapter pattern to integrate external traffic service
        var externalService = new ExternalTrafficService();
        _trafficDataProvider = new TrafficServiceAdapter(externalService);
    }

    public void Start()
    {
        _isRunning = true;
        foreach (var light in _trafficLights)
        {
            light.Activate();
        }
        Console.WriteLine($"{Name} started.");
    }

    public void Stop()
    {
        _isRunning = false;
        foreach (var light in _trafficLights)
        {
            light.Deactivate();
        }
        Console.WriteLine($"{Name} stopped.");
    }

    public string GetStatus()
    {
        var activeCount = _trafficLights.Count(l => 
        {
            var info = l.GetInfo();
            return info.Contains("Operating");
        });
        
        return $"Status: {(_isRunning ? "Running" : "Stopped")}\n" +
               $"Traffic Lights: {_trafficLights.Count}\n" +
               $"Active Lights: {activeCount}\n" +
               $"Traffic Status: {_currentTrafficStatus}";
    }

    /// <summary>
    /// Displays current traffic status using external data
    /// </summary>
    public void DisplayTrafficStatus()
    {
        Console.WriteLine("\n--- Traffic Status ---");
        try
        {
            var trafficData = _trafficDataProvider.GetTrafficData();
            Console.WriteLine(trafficData.ToString());
            _currentTrafficStatus = trafficData.Status;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching traffic data: {ex.Message}");
        }
        Console.WriteLine("----------------------\n");
    }

    /// <summary>
    /// Displays all traffic lights status
    /// </summary>
    public void DisplayTrafficLights()
    {
        Console.WriteLine("\n--- Traffic Lights ---");
        foreach (var light in _trafficLights)
        {
            Console.WriteLine(light.GetInfo());
        }
        Console.WriteLine("----------------------\n");
    }

    /// <summary>
    /// Optimizes traffic flow
    /// </summary>
    public void OptimizeTrafficFlow()
    {
        Console.WriteLine("Analyzing traffic patterns...");
        Console.WriteLine("Adjusting traffic light timings...");
        Console.WriteLine("Traffic flow optimized.");
    }

    /// <summary>
    /// Adds a new traffic light
    /// </summary>
    public void AddTrafficLight(IInfrastructureDevice light)
    {
        _trafficLights.Add(light);
        Console.WriteLine($"Added traffic light: {light.Id}");
    }
}
