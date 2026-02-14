using Xunit;
using SmartCitySystem.Core;
using SmartCitySystem.Core.Singleton;
using SmartCitySystem.Core.Factories;
using SmartCitySystem.Core.Builders;
using SmartCitySystem.Core.Proxy;
using SmartCitySystem.Core.Adapters;
using SmartCitySystem.Modules.Lighting;
using SmartCitySystem.Modules.Transport;
using SmartCitySystem.Modules.Security;
using SmartCitySystem.Modules.Energy;

namespace SmartCitySystem.Tests;

/// <summary>
/// Unit tests for Smart City System
/// Tests all design patterns and core functionality
/// </summary>
public class SmartCitySystemTests
{
    /// <summary>
    /// Test 1: Singleton Pattern - Ensures only one instance exists
    /// </summary>
    [Fact]
    public void SingletonPattern_ShouldReturnSameInstance()
    {
        // Arrange & Act
        var instance1 = SmartCityController.Instance;
        var instance2 = SmartCityController.Instance;

        // Assert
        Assert.NotNull(instance1);
        Assert.NotNull(instance2);
        Assert.Same(instance1, instance2);
    }

    /// <summary>
    /// Test 2: Factory Pattern - Should create correct device types
    /// </summary>
    [Fact]
    public void FactoryPattern_ShouldCreateCorrectDeviceTypes()
    {
        // Arrange
        var lightingFactory = new LightingDeviceFactory();
        var transportFactory = new TransportDeviceFactory();
        var securityFactory = new SecurityDeviceFactory();
        var energyFactory = new EnergyDeviceFactory();

        // Act
        var streetLight = lightingFactory.CreateDevice("L-001");
        var trafficLight = transportFactory.CreateDevice("T-001");
        var camera = securityFactory.CreateDevice("S-001");
        var meter = energyFactory.CreateDevice("E-001");

        // Assert
        Assert.NotNull(streetLight);
        Assert.NotNull(trafficLight);
        Assert.NotNull(camera);
        Assert.NotNull(meter);
        Assert.Equal("Street Light", streetLight.Type);
        Assert.Equal("Traffic Light", trafficLight.Type);
        Assert.Equal("Security Camera", camera.Type);
        Assert.Equal("Energy Meter", meter.Type);
    }

    /// <summary>
    /// Test 3: Builder Pattern - Should build city configuration step by step
    /// </summary>
    [Fact]
    public void BuilderPattern_ShouldBuildCityConfiguration()
    {
        // Arrange
        var builder = new CityConfigurationBuilder();

        // Act
        var config = builder
            .SetCityName("Test City")
            .SetPopulation(100000)
            .SetArea(50.0)
            .AddLightingInfrastructure(5)
            .AddTransportInfrastructure(3)
            .AddSecurityInfrastructure(4)
            .AddEnergyInfrastructure(2)
            .Build();

        // Assert
        Assert.NotNull(config);
        Assert.Equal("Test City", config.CityName);
        Assert.Equal(100000, config.Population);
        Assert.Equal(50.0, config.Area);
        Assert.Equal(5, config.LightingDevices.Count);
        Assert.Equal(3, config.TransportDevices.Count);
        Assert.Equal(4, config.SecurityDevices.Count);
        Assert.Equal(2, config.EnergyDevices.Count);
    }

    /// <summary>
    /// Test 4: Builder with Director - Should create predefined configurations
    /// </summary>
    [Fact]
    public void BuilderWithDirector_ShouldCreatePredefinedConfigurations()
    {
        // Arrange
        var builder = new CityConfigurationBuilder();
        var director = new CityDirector(builder);

        // Act
        var smallCity = director.BuildSmallCity("Small Town");
        var largeCity = director.BuildLargeCity("Metropolis");

        // Assert
        Assert.NotNull(smallCity);
        Assert.NotNull(largeCity);
        Assert.Equal("Small Town", smallCity.CityName);
        Assert.Equal("Metropolis", largeCity.CityName);
        Assert.True(largeCity.Population > smallCity.Population);
        Assert.True(largeCity.LightingDevices.Count > smallCity.LightingDevices.Count);
    }

    /// <summary>
    /// Test 5: Proxy Pattern - Should control access based on authorization
    /// </summary>
    [Fact]
    public void ProxyPattern_ShouldControlAccessBasedOnAuthorization()
    {
        // Arrange
        var proxy = new SecuritySubsystemProxy();

        // Act
        var authorizedResult = proxy.AccessData("admin");
        var unauthorizedResult = proxy.AccessData("guest");

        // Assert
        Assert.Contains("REAL SYSTEM", authorizedResult);
        Assert.Contains("Access denied", unauthorizedResult);
    }

    /// <summary>
    /// Test 6: Adapter Pattern - Should adapt weather service interface
    /// </summary>
    [Fact]
    public void AdapterPattern_ShouldAdaptWeatherServiceInterface()
    {
        // Arrange
        var legacyService = new LegacyWeatherService();
        var adapter = new WeatherServiceAdapter(legacyService);

        // Act
        var weatherData = adapter.GetWeatherData();

        // Assert
        Assert.NotNull(weatherData);
        Assert.True(weatherData.Temperature > 0);
        Assert.True(weatherData.Humidity > 0);
        Assert.NotEmpty(weatherData.WindSpeed);
    }

    /// <summary>
    /// Test 7: Adapter Pattern - Should adapt traffic service interface
    /// </summary>
    [Fact]
    public void AdapterPattern_ShouldAdaptTrafficServiceInterface()
    {
        // Arrange
        var externalService = new ExternalTrafficService();
        var adapter = new TrafficServiceAdapter(externalService);

        // Act
        var trafficData = adapter.GetTrafficData();

        // Assert
        Assert.NotNull(trafficData);
        Assert.NotEmpty(trafficData.Status);
        Assert.NotEmpty(trafficData.Location);
        Assert.True(trafficData.VehicleCount >= 0);
    }

    /// <summary>
    /// Test 8: Lighting Subsystem - Should start and stop correctly
    /// </summary>
    [Fact]
    public void LightingSubsystem_ShouldStartAndStop()
    {
        // Arrange
        var lighting = new LightingSubsystem();

        // Act
        lighting.Start();
        var statusAfterStart = lighting.GetStatus();
        lighting.Stop();
        var statusAfterStop = lighting.GetStatus();

        // Assert
        Assert.Contains("Running", statusAfterStart);
        Assert.Contains("Stopped", statusAfterStop);
    }

    /// <summary>
    /// Test 9: Transport Subsystem - Should manage traffic lights
    /// </summary>
    [Fact]
    public void TransportSubsystem_ShouldManageTrafficLights()
    {
        // Arrange
        var transport = new TransportSubsystem();

        // Act
        transport.Start();
        var status = transport.GetStatus();

        // Assert
        Assert.Contains("Running", status);
        Assert.Contains("Traffic Lights:", status);
    }

    /// <summary>
    /// Test 10: Security Subsystem - Should manage cameras and alerts
    /// </summary>
    [Fact]
    public void SecuritySubsystem_ShouldManageCamerasAndAlerts()
    {
        // Arrange
        var security = new SecuritySubsystem();

        // Act
        security.Start();
        var status = security.GetStatus();

        // Assert
        Assert.Contains("Running", status);
        Assert.Contains("Security Cameras:", status);
    }

    /// <summary>
    /// Test 11: Energy Subsystem - Should monitor consumption
    /// </summary>
    [Fact]
    public void EnergySubsystem_ShouldMonitorConsumption()
    {
        // Arrange
        var energy = new EnergySubsystem();

        // Act
        energy.Start();
        var status = energy.GetStatus();

        // Assert
        Assert.Contains("Running", status);
        Assert.Contains("Energy Meters:", status);
        Assert.Contains("Consumption:", status);
    }

    /// <summary>
    /// Test 12: Facade Pattern - Should provide simplified interface
    /// </summary>
    [Fact]
    public void FacadePattern_ShouldProvideSimplifiedInterface()
    {
        // Arrange
        var facade = new SmartCityFacade();

        // Act
        facade.InitializeCity();
        var controller = SmartCityController.Instance;

        // Assert
        Assert.True(controller.IsRunning);
        Assert.True(controller.Subsystems.Count >= 4); // At least 4 subsystems registered
    }

    /// <summary>
    /// Test 13: Infrastructure Device - Should activate and deactivate
    /// </summary>
    [Fact]
    public void InfrastructureDevice_ShouldActivateAndDeactivate()
    {
        // Arrange
        var factory = new LightingDeviceFactory();
        var device = factory.CreateDevice("TEST-001");

        // Act
        device.Activate();
        var activeInfo = device.GetInfo();
        device.Deactivate();
        var inactiveInfo = device.GetInfo();

        // Assert
        Assert.Contains("ON", activeInfo);
        Assert.Contains("OFF", inactiveInfo);
    }

    /// <summary>
    /// Test 14: Controller - Should register and manage subsystems
    /// </summary>
    [Fact]
    public void Controller_ShouldRegisterAndManageSubsystems()
    {
        // Arrange
        var controller = SmartCityController.Instance;
        var lighting = new LightingSubsystem();
        var initialCount = controller.Subsystems.Count;

        // Act
        controller.RegisterSubsystem(lighting);

        // Assert
        Assert.Contains(lighting, controller.Subsystems);
    }

    /// <summary>
    /// Test 15: Energy Optimization - Should reduce consumption
    /// </summary>
    [Fact]
    public void EnergyOptimization_ShouldReduceConsumption()
    {
        // Arrange
        var energy = new EnergySubsystem();
        energy.Start();
        var statusBefore = energy.GetStatus();

        // Act
        energy.OptimizeConsumption();
        var statusAfter = energy.GetStatus();

        // Assert
        Assert.Contains("Optimization: Disabled", statusBefore);
        Assert.Contains("Optimization: Enabled", statusAfter);
    }
}
