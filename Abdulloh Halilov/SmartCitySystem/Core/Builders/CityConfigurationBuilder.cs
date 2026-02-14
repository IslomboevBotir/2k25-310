using SmartCitySystem.Core.Factories;

namespace SmartCitySystem.Core.Builders;

/// <summary>
/// City Configuration class - complex object built by Builder
/// </summary>
public class CityConfiguration
{
    public string CityName { get; set; } = "Unnamed City";
    public List<IInfrastructureDevice> LightingDevices { get; set; } = new();
    public List<IInfrastructureDevice> TransportDevices { get; set; } = new();
    public List<IInfrastructureDevice> SecurityDevices { get; set; } = new();
    public List<IInfrastructureDevice> EnergyDevices { get; set; } = new();
    public int Population { get; set; }
    public double Area { get; set; }

    public void DisplayConfiguration()
    {
        Console.WriteLine($"\n=== City Configuration: {CityName} ===");
        Console.WriteLine($"Population: {Population:N0}");
        Console.WriteLine($"Area: {Area} sq km");
        Console.WriteLine($"Lighting Devices: {LightingDevices.Count}");
        Console.WriteLine($"Transport Devices: {TransportDevices.Count}");
        Console.WriteLine($"Security Devices: {SecurityDevices.Count}");
        Console.WriteLine($"Energy Devices: {EnergyDevices.Count}");
        Console.WriteLine("====================================\n");
    }
}

/// <summary>
/// Builder Pattern: Interface for building city configurations
/// </summary>
public interface ICityBuilder
{
    ICityBuilder SetCityName(string name);
    ICityBuilder SetPopulation(int population);
    ICityBuilder SetArea(double area);
    ICityBuilder AddLightingInfrastructure(int count);
    ICityBuilder AddTransportInfrastructure(int count);
    ICityBuilder AddSecurityInfrastructure(int count);
    ICityBuilder AddEnergyInfrastructure(int count);
    CityConfiguration Build();
}

/// <summary>
/// Builder Pattern: Concrete builder for creating city configurations
/// Allows step-by-step construction of complex city objects
/// </summary>
public class CityConfigurationBuilder : ICityBuilder
{
    private CityConfiguration _configuration;
    private readonly LightingDeviceFactory _lightingFactory;
    private readonly TransportDeviceFactory _transportFactory;
    private readonly SecurityDeviceFactory _securityFactory;
    private readonly EnergyDeviceFactory _energyFactory;

    public CityConfigurationBuilder()
    {
        _configuration = new CityConfiguration();
        _lightingFactory = new LightingDeviceFactory();
        _transportFactory = new TransportDeviceFactory();
        _securityFactory = new SecurityDeviceFactory();
        _energyFactory = new EnergyDeviceFactory();
    }

    public ICityBuilder SetCityName(string name)
    {
        _configuration.CityName = name;
        return this;
    }

    public ICityBuilder SetPopulation(int population)
    {
        _configuration.Population = population;
        return this;
    }

    public ICityBuilder SetArea(double area)
    {
        _configuration.Area = area;
        return this;
    }

    public ICityBuilder AddLightingInfrastructure(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var device = _lightingFactory.CreateDevice($"L-{i + 1:D3}");
            _configuration.LightingDevices.Add(device);
        }
        return this;
    }

    public ICityBuilder AddTransportInfrastructure(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var device = _transportFactory.CreateDevice($"T-{i + 1:D3}");
            _configuration.TransportDevices.Add(device);
        }
        return this;
    }

    public ICityBuilder AddSecurityInfrastructure(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var device = _securityFactory.CreateDevice($"S-{i + 1:D3}");
            _configuration.SecurityDevices.Add(device);
        }
        return this;
    }

    public ICityBuilder AddEnergyInfrastructure(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var device = _energyFactory.CreateDevice($"E-{i + 1:D3}");
            _configuration.EnergyDevices.Add(device);
        }
        return this;
    }

    public CityConfiguration Build()
    {
        var result = _configuration;
        // Reset the configuration for next build
        _configuration = new CityConfiguration();
        return result;
    }
}

/// <summary>
/// Director: Orchestrates the building process for predefined configurations
/// </summary>
public class CityDirector
{
    private readonly ICityBuilder _builder;

    public CityDirector(ICityBuilder builder)
    {
        _builder = builder;
    }

    /// <summary>
    /// Builds a small city configuration
    /// </summary>
    public CityConfiguration BuildSmallCity(string name)
    {
        return _builder
            .SetCityName(name)
            .SetPopulation(50000)
            .SetArea(25.5)
            .AddLightingInfrastructure(10)
            .AddTransportInfrastructure(5)
            .AddSecurityInfrastructure(8)
            .AddEnergyInfrastructure(3)
            .Build();
    }

    /// <summary>
    /// Builds a large city configuration
    /// </summary>
    public CityConfiguration BuildLargeCity(string name)
    {
        return _builder
            .SetCityName(name)
            .SetPopulation(500000)
            .SetArea(150.0)
            .AddLightingInfrastructure(50)
            .AddTransportInfrastructure(30)
            .AddSecurityInfrastructure(40)
            .AddEnergyInfrastructure(20)
            .Build();
    }
}
