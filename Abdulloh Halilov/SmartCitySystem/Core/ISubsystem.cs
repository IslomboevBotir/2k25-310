namespace SmartCitySystem.Core;

/// <summary>
/// Interface for all smart city subsystems
/// </summary>
public interface ISubsystem
{
    /// <summary>
    /// Gets the name of the subsystem
    /// </summary>
    string Name { get; }
    
    /// <summary>
    /// Gets the current status of the subsystem
    /// </summary>
    string GetStatus();
    
    /// <summary>
    /// Starts the subsystem
    /// </summary>
    void Start();
    
    /// <summary>
    /// Stops the subsystem
    /// </summary>
    void Stop();
}
