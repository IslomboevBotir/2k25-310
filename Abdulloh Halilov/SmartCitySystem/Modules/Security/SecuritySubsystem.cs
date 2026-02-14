using SmartCitySystem.Core;
using SmartCitySystem.Core.Factories;
using SmartCitySystem.Core.Proxy;

namespace SmartCitySystem.Modules.Security;

/// <summary>
/// Security Management Subsystem
/// Manages security cameras, alerts, and access control
/// Uses Proxy pattern for secure access
/// </summary>
public class SecuritySubsystem : ISubsystem
{
    private readonly List<IInfrastructureDevice> _cameras;
    private readonly SecuritySubsystemProxy _securityProxy;
    private readonly List<string> _alerts;
    private bool _isRunning;

    public string Name => "Security Management System";

    public SecuritySubsystem()
    {
        _cameras = new List<IInfrastructureDevice>();
        _securityProxy = new SecuritySubsystemProxy();
        _alerts = new List<string>();
        _isRunning = false;
        
        // Initialize with security cameras
        var factory = new SecurityDeviceFactory();
        for (int i = 1; i <= 6; i++)
        {
            _cameras.Add(factory.CreateDevice($"CAM-{i:D3}"));
        }

        // Add some sample alerts
        _alerts.Add("Motion detected at North Gate - 02:30 AM");
        _alerts.Add("Unauthorized access attempt - Parking Lot B - 11:45 PM");
    }

    public void Start()
    {
        _isRunning = true;
        foreach (var camera in _cameras)
        {
            camera.Activate();
        }
        Console.WriteLine($"{Name} started.");
    }

    public void Stop()
    {
        _isRunning = false;
        foreach (var camera in _cameras)
        {
            camera.Deactivate();
        }
        Console.WriteLine($"{Name} stopped.");
    }

    public string GetStatus()
    {
        var activeCount = _cameras.Count(c => 
        {
            var info = c.GetInfo();
            return info.Contains("Recording");
        });
        
        return $"Status: {(_isRunning ? "Running" : "Stopped")}\n" +
               $"Security Cameras: {_cameras.Count}\n" +
               $"Active Cameras: {activeCount}\n" +
               $"Pending Alerts: {_alerts.Count}";
    }

    /// <summary>
    /// Checks and displays security alerts
    /// </summary>
    public void CheckAlerts()
    {
        Console.WriteLine("\n--- Security Alerts ---");
        if (_alerts.Count == 0)
        {
            Console.WriteLine("No alerts.");
        }
        else
        {
            foreach (var alert in _alerts)
            {
                Console.WriteLine($"⚠️  {alert}");
            }
        }
        Console.WriteLine("-----------------------\n");
    }

    /// <summary>
    /// Accesses a security camera (uses Proxy for access control)
    /// </summary>
    public void AccessCamera(string userId, string cameraId)
    {
        Console.WriteLine($"\nAttempting to access camera {cameraId}...");
        var result = _securityProxy.AccessData(userId);
        Console.WriteLine(result);
        
        // If access granted, show camera info
        if (result.Contains("REAL SYSTEM"))
        {
            var camera = _cameras.FirstOrDefault(c => c.Id == cameraId);
            if (camera != null)
            {
                Console.WriteLine($"Camera Feed: {camera.GetInfo()}");
            }
            else
            {
                Console.WriteLine($"Camera {cameraId} not found.");
            }
        }
    }

    /// <summary>
    /// Modifies security settings (uses Proxy for access control)
    /// </summary>
    public void ModifySecuritySettings(string userId, string setting)
    {
        Console.WriteLine($"\nAttempting to modify security settings...");
        _securityProxy.ModifySettings(userId, setting);
    }

    /// <summary>
    /// Displays all cameras status
    /// </summary>
    public void DisplayCamerasStatus()
    {
        Console.WriteLine("\n--- Security Cameras ---");
        foreach (var camera in _cameras)
        {
            Console.WriteLine(camera.GetInfo());
        }
        Console.WriteLine("------------------------\n");
    }

    /// <summary>
    /// Displays the security access log
    /// </summary>
    public void DisplayAccessLog()
    {
        _securityProxy.DisplayAccessLog();
    }

    /// <summary>
    /// Adds a security alert
    /// </summary>
    public void AddAlert(string alert)
    {
        _alerts.Add(alert);
        Console.WriteLine($"Alert added: {alert}");
    }

    /// <summary>
    /// Clears all alerts
    /// </summary>
    public void ClearAlerts()
    {
        _alerts.Clear();
        Console.WriteLine("All alerts cleared.");
    }

    /// <summary>
    /// Gets the security proxy for advanced operations
    /// </summary>
    public SecuritySubsystemProxy GetSecurityProxy() => _securityProxy;
}
