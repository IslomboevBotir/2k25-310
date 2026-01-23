using System.Collections.Generic;
using SmartCity.Adapters;
using SmartCity.Builders;
using SmartCity.Core;
using SmartCity.Factories;
using SmartCity.Modules;
using SmartCity.Proxies;
using Xunit;

namespace SmartCity.Tests;

public sealed class FactoryTests
{
    [Fact]
    public void Factory_Creates_ConcreteSubsystems()
    {
        var config = new SmartCityConfigurationBuilder()
            .WithInitialLightingMode(LightingMode.On)
            .WithInitialRoutes(new List<string> { "Line A" })
            .WithEnergyAlertThresholdKw(600)
            .WithSecurityArmed(true)
            .WithAllowedSecurityRoles(new List<string> { "Admin" })
            .Build();

        var factory = new SmartCitySubsystemFactory(
            new FakeEnergyService(),
            new RoleBasedAccessPolicy(config.AllowedSecurityRoles),
            new UserContext("Admin"));

        Assert.IsType<LightingSubsystem>(factory.CreateLightingSubsystem(config));
        Assert.IsType<TransportationSubsystem>(factory.CreateTransportationSubsystem(config));
        Assert.IsType<EnergySubsystem>(factory.CreateEnergySubsystem(config));
        Assert.IsType<SecurityProxy>(factory.CreateSecuritySubsystem(config));
    }

    private sealed class FakeEnergyService : IExternalEnergyService
    {
        public int GetCurrentUsageKw() => 500;
    }
}