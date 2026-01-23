using SmartCity.Adapters;
using SmartCity.Core;

namespace SmartCity.Modules;

public sealed class EnergySubsystem : IEnergySubsystem
{
    private readonly IExternalEnergyService _externalEnergyService;

    public EnergySubsystem(IExternalEnergyService externalEnergyService, int alertThresholdKw)
    {
        _externalEnergyService = externalEnergyService;
        AlertThresholdKw = alertThresholdKw;
    }

    public int AlertThresholdKw { get; }

    public int CurrentUsageKw => _externalEnergyService.GetCurrentUsageKw();

    public string GetStatus()
    {
        var current = CurrentUsageKw;
        var status = current > AlertThresholdKw ? "Alert" : "Normal";
        return $"Energy: Usage={current}kW Threshold={AlertThresholdKw}kW Status={status}";
    }
}