using SmartCitySystem.Core.Singleton;
using SmartCitySystem.Modules.Lighting;
using SmartCitySystem.Modules.Transport;
using SmartCitySystem.Modules.Security;
using SmartCitySystem.Modules.Energy;

namespace SmartCitySystem.Core;

/// <summary>
/// Facade Pattern: Provides a simplified interface to the complex Smart City subsystems
/// Hides the complexity of the subsystem interactions from clients
/// This is the main interface for users to interact with the system
/// </summary>
public class SmartCityFacade
{
    private readonly SmartCityController _controller;
    private readonly LightingSubsystem _lightingSystem;
    private readonly TransportSubsystem _transportSystem;
    private readonly SecuritySubsystem _securitySystem;
    private readonly EnergySubsystem _energySystem;

    public SmartCityFacade()
    {
        _controller = SmartCityController.Instance;
        _lightingSystem = new LightingSubsystem();
        _transportSystem = new TransportSubsystem();
        _securitySystem = new SecuritySubsystem();
        _energySystem = new EnergySubsystem();

        // Register all subsystems with the controller
        _controller.RegisterSubsystem(_lightingSystem);
        _controller.RegisterSubsystem(_transportSystem);
        _controller.RegisterSubsystem(_securitySystem);
        _controller.RegisterSubsystem(_energySystem);
    }

    /// <summary>
    /// Initializes the entire Smart City system
    /// </summary>
    public void InitializeCity()
    {
        Console.WriteLine("Initializing Smart City System...");
        _controller.StartAllSystems();
    }

    /// <summary>
    /// Shuts down the entire Smart City system
    /// </summary>
    public void ShutdownCity()
    {
        Console.WriteLine("Shutting down Smart City System...");
        _controller.StopAllSystems();
    }

    /// <summary>
    /// Displays status of all city systems
    /// </summary>
    public void DisplaySystemStatus()
    {
        _controller.DisplayAllStatus();
    }

    /// <summary>
    /// Controls the lighting subsystem
    /// </summary>
    public void ControlLighting(bool turnOn)
    {
        if (turnOn)
        {
            _lightingSystem.TurnOnAllLights();
        }
        else
        {
            _lightingSystem.TurnOffAllLights();
        }
    }

    /// <summary>
    /// Adjusts lighting brightness
    /// </summary>
    public void AdjustLightingBrightness(int brightness)
    {
        _lightingSystem.SetBrightness(brightness);
    }

    /// <summary>
    /// Monitors transportation status
    /// </summary>
    public void MonitorTransportation()
    {
        _transportSystem.DisplayTrafficStatus();
    }

    /// <summary>
    /// Manages security alerts
    /// </summary>
    public void CheckSecurityAlerts()
    {
        _securitySystem.CheckAlerts();
    }

    /// <summary>
    /// Access security cameras
    /// </summary>
    public void AccessSecurityCamera(string userId, string cameraId)
    {
        _securitySystem.AccessCamera(userId, cameraId);
    }

    /// <summary>
    /// Monitors energy consumption
    /// </summary>
    public void MonitorEnergy()
    {
        _energySystem.DisplayEnergyConsumption();
    }

    /// <summary>
    /// Optimizes energy usage
    /// </summary>
    public void OptimizeEnergy()
    {
        _energySystem.OptimizeConsumption();
    }

    /// <summary>
    /// Gets the lighting subsystem for direct access if needed
    /// </summary>
    public LightingSubsystem GetLightingSystem() => _lightingSystem;

    /// <summary>
    /// Gets the transport subsystem for direct access if needed
    /// </summary>
    public TransportSubsystem GetTransportSystem() => _transportSystem;

    /// <summary>
    /// Gets the security subsystem for direct access if needed
    /// </summary>
    public SecuritySubsystem GetSecuritySystem() => _securitySystem;

    /// <summary>
    /// Gets the energy subsystem for direct access if needed
    /// </summary>
    public EnergySubsystem GetEnergySystem() => _energySystem;
}
