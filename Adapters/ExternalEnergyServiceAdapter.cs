namespace SmartCity.Adapters;

public interface IExternalEnergyService
{
    int GetCurrentUsageKw();
}

public interface ILegacyEnergyService
{
    double ReadConsumption();
}

/// <summary>
/// Adapter: converts a legacy energy service API into the interface used by the Energy subsystem.
/// </summary>
public sealed class ExternalEnergyServiceAdapter : IExternalEnergyService
{
    private readonly ILegacyEnergyService _legacyService;

    public ExternalEnergyServiceAdapter(ILegacyEnergyService legacyService)
    {
        _legacyService = legacyService;
    }

    public int GetCurrentUsageKw()
    {
        var value = _legacyService.ReadConsumption();
        return (int)System.Math.Round(value);
    }
}

public sealed class LegacyEnergyService : ILegacyEnergyService
{
    private readonly System.Random _random = new();

    public double ReadConsumption()
    {
        return 500 + _random.NextDouble() * 400;
    }
}