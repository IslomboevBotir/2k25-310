using System;
using SmartCity.Factories;

namespace SmartCity.Core;

/// <summary>
/// Singleton + Facade: ensures a single controller instance and provides a unified entry point for all subsystems,
/// reducing coupling between the UI and internal modules.
/// </summary>
public sealed class SmartCityController
{
    private static readonly Lazy<SmartCityController> LazyInstance =
        new(() => new SmartCityController());

    private bool _initialized;
    private ILightingSubsystem? _lighting;
    private ITransportationSubsystem? _transport;
    private ISecuritySubsystem? _security;
    private IEnergySubsystem? _energy;

    private SmartCityController()
    {
    }

    public static SmartCityController Instance => LazyInstance.Value;

    public void Initialize(SmartCityConfiguration config, ISubsystemFactory factory)
    {
        if (_initialized)
        {
            return;
        }

        _lighting = factory.CreateLightingSubsystem(config);
        _transport = factory.CreateTransportationSubsystem(config);
        _security = factory.CreateSecuritySubsystem(config);
        _energy = factory.CreateEnergySubsystem(config);
        _initialized = true;
    }

    public void SetLightingMode(Modules.LightingMode mode)
    {
        EnsureInitialized();
        _lighting!.SetMode(mode);
    }

    public string GetLightingStatus()
    {
        EnsureInitialized();
        return _lighting!.GetStatus();
    }

    public void AddTransportRoute(string route)
    {
        EnsureInitialized();
        if (!string.IsNullOrWhiteSpace(route))
        {
            _transport!.AddRoute(route);
        }
    }

    public void RemoveTransportRoute(string route)
    {
        EnsureInitialized();
        if (!string.IsNullOrWhiteSpace(route))
        {
            _transport!.RemoveRoute(route);
        }
    }

    public string GetTransportStatus()
    {
        EnsureInitialized();
        return _transport!.GetStatus();
    }

    public string GetEnergyReport()
    {
        EnsureInitialized();
        return _energy!.GetStatus();
    }

    public void ArmSecurity()
    {
        EnsureInitialized();
        _security!.Arm();
    }

    public void DisarmSecurity()
    {
        EnsureInitialized();
        _security!.Disarm();
    }

    public string GetSecurityStatus()
    {
        EnsureInitialized();
        return _security!.GetStatus();
    }

    private void EnsureInitialized()
    {
        if (!_initialized)
        {
            throw new InvalidOperationException("Controller is not initialized.");
        }
    }
}