using System.Collections.Generic;
using SmartCity.Core;
using SmartCity.Modules;

namespace SmartCity.Builders;

/// <summary>
/// Builder: assembles a complex configuration step-by-step, keeping construction logic readable and extensible.
/// </summary>
public sealed class SmartCityConfigurationBuilder
{
    private readonly SmartCityConfiguration _config = new();

    public SmartCityConfigurationBuilder WithCityName(string cityName)
    {
        _config.CityName = cityName;
        return this;
    }

    public SmartCityConfigurationBuilder WithInitialLightingMode(LightingMode mode)
    {
        _config.InitialLightingMode = mode;
        return this;
    }

    public SmartCityConfigurationBuilder WithInitialRoutes(IReadOnlyList<string> routes)
    {
        _config.InitialRoutes = routes;
        return this;
    }

    public SmartCityConfigurationBuilder WithEnergyAlertThresholdKw(int threshold)
    {
        _config.EnergyAlertThresholdKw = threshold;
        return this;
    }

    public SmartCityConfigurationBuilder WithSecurityArmed(bool armed)
    {
        _config.SecurityArmed = armed;
        return this;
    }

    public SmartCityConfigurationBuilder WithAllowedSecurityRoles(IReadOnlyList<string> roles)
    {
        _config.AllowedSecurityRoles = roles;
        return this;
    }

    public SmartCityConfiguration Build()
    {
        return _config;
    }
}