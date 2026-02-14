namespace SmartCitySystem.Core.Factories;

/// <summary>
/// Base interface for infrastructure devices
/// </summary>
public interface IInfrastructureDevice
{
    string Type { get; }
    string Id { get; }
    void Activate();
    void Deactivate();
    string GetInfo();
}

/// <summary>
/// Abstract Factory Pattern: Base factory for creating infrastructure devices
/// Defines the interface for creating families of related devices
/// </summary>
public abstract class InfrastructureFactory
{
    /// <summary>
    /// Factory method for creating infrastructure devices
    /// </summary>
    public abstract IInfrastructureDevice CreateDevice(string id);
}

/// <summary>
/// Concrete Factory: Creates lighting devices
/// </summary>
public class LightingDeviceFactory : InfrastructureFactory
{
    public override IInfrastructureDevice CreateDevice(string id)
    {
        return new StreetLight(id);
    }
}

/// <summary>
/// Concrete Factory: Creates transportation devices
/// </summary>
public class TransportDeviceFactory : InfrastructureFactory
{
    public override IInfrastructureDevice CreateDevice(string id)
    {
        return new TrafficLight(id);
    }
}

/// <summary>
/// Concrete Factory: Creates security devices
/// </summary>
public class SecurityDeviceFactory : InfrastructureFactory
{
    public override IInfrastructureDevice CreateDevice(string id)
    {
        return new SecurityCamera(id);
    }
}

/// <summary>
/// Concrete Factory: Creates energy monitoring devices
/// </summary>
public class EnergyDeviceFactory : InfrastructureFactory
{
    public override IInfrastructureDevice CreateDevice(string id)
    {
        return new EnergyMeter(id);
    }
}

// Concrete Product: Street Light
public class StreetLight : IInfrastructureDevice
{
    public string Type => "Street Light";
    public string Id { get; }
    private bool _isActive;

    public StreetLight(string id)
    {
        Id = id;
        _isActive = false;
    }

    public void Activate()
    {
        _isActive = true;
    }

    public void Deactivate()
    {
        _isActive = false;
    }

    public string GetInfo()
    {
        return $"Street Light {Id}: {(_isActive ? "ON" : "OFF")}";
    }
}

// Concrete Product: Traffic Light
public class TrafficLight : IInfrastructureDevice
{
    public string Type => "Traffic Light";
    public string Id { get; }
    private bool _isActive;

    public TrafficLight(string id)
    {
        Id = id;
        _isActive = false;
    }

    public void Activate()
    {
        _isActive = true;
    }

    public void Deactivate()
    {
        _isActive = false;
    }

    public string GetInfo()
    {
        return $"Traffic Light {Id}: {(_isActive ? "Operating" : "Inactive")}";
    }
}

// Concrete Product: Security Camera
public class SecurityCamera : IInfrastructureDevice
{
    public string Type => "Security Camera";
    public string Id { get; }
    private bool _isActive;

    public SecurityCamera(string id)
    {
        Id = id;
        _isActive = false;
    }

    public void Activate()
    {
        _isActive = true;
    }

    public void Deactivate()
    {
        _isActive = false;
    }

    public string GetInfo()
    {
        return $"Security Camera {Id}: {(_isActive ? "Recording" : "Standby")}";
    }
}

// Concrete Product: Energy Meter
public class EnergyMeter : IInfrastructureDevice
{
    public string Type => "Energy Meter";
    public string Id { get; }
    private bool _isActive;

    public EnergyMeter(string id)
    {
        Id = id;
        _isActive = false;
    }

    public void Activate()
    {
        _isActive = true;
    }

    public void Deactivate()
    {
        _isActive = false;
    }

    public string GetInfo()
    {
        return $"Energy Meter {Id}: {(_isActive ? "Monitoring" : "Offline")}";
    }
}
