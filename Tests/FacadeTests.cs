using System.Collections.Generic;
using SmartCity.Adapters;
using SmartCity.Builders;
using SmartCity.Core;
using SmartCity.Factories;
using SmartCity.Modules;
using SmartCity.Proxies;
using Xunit;

namespace SmartCity.Tests;

public sealed class FacadeTests
{
    [Fact]
    public void Facade_Controls_Lighting_And_Transport()
    {
        var controller = SmartCityController.Instance;
        controller.Initialize(BuildConfig(), BuildFactory());

        controller.SetLightingMode(LightingMode.Off);
        Assert.Contains("Mode=Off", controller.GetLightingStatus());

        controller.AddTransportRoute("Line B");
        Assert.Contains("Line B", controller.GetTransportStatus());
    }

    private static SmartCityConfiguration BuildConfig()
    {
        return new SmartCityConfigurationBuilder()
            .WithInitialLightingMode(LightingMode.Auto)
            .WithInitialRoutes(new List<string>())
            .WithEnergyAlertThresholdKw(600)
            .WithSecurityArmed(false)
            .WithAllowedSecurityRoles(new List<string> { "Admin" })
            .Build();
    }

    private static ISubsystemFactory BuildFactory()
    {
        return new SmartCitySubsystemFactory(
            new FakeEnergyService(),
            new RoleBasedAccessPolicy(new List<string> { "Admin" }),
            new UserContext("Admin"));
    }

    private sealed class FakeEnergyService : IExternalEnergyService
    {
        public int GetCurrentUsageKw() => 500;
    }
}