using SmartCity.Adapters;
using SmartCity.Core;
using SmartCity.Modules;
using SmartCity.Proxies;

namespace SmartCity.Factories;

/// <summary>
/// Factory Method: centralizes subsystem creation so the controller stays decoupled from concrete implementations.
/// </summary>
public sealed class SmartCitySubsystemFactory : ISubsystemFactory
{
    private readonly IExternalEnergyService _externalEnergyService;
    private readonly IAccessPolicy _accessPolicy;
    private readonly IUserContext _userContext;

    public SmartCitySubsystemFactory(
        IExternalEnergyService externalEnergyService,
        IAccessPolicy accessPolicy,
        IUserContext userContext)
    {
        _externalEnergyService = externalEnergyService;
        _accessPolicy = accessPolicy;
        _userContext = userContext;
    }

    public ILightingSubsystem CreateLightingSubsystem(SmartCityConfiguration config)
    {
        return new LightingSubsystem(config.InitialLightingMode);
    }

    public ITransportationSubsystem CreateTransportationSubsystem(SmartCityConfiguration config)
    {
        return new TransportationSubsystem(config.InitialRoutes);
    }

    public ISecuritySubsystem CreateSecuritySubsystem(SmartCityConfiguration config)
    {
        var core = new SecuritySubsystem(config.SecurityArmed);
        return new SecurityProxy(core, _accessPolicy, _userContext);
    }

    public IEnergySubsystem CreateEnergySubsystem(SmartCityConfiguration config)
    {
        return new EnergySubsystem(_externalEnergyService, config.EnergyAlertThresholdKw);
    }
}