using System.Collections.Generic;
using SmartCity.Modules;

namespace SmartCity.Core;

public interface ILightingSubsystem : ISubsystem
{
    void SetMode(LightingMode mode);
    LightingMode CurrentMode { get; }
}

public interface ITransportationSubsystem : ISubsystem
{
    void AddRoute(string name);
    void RemoveRoute(string name);
    IReadOnlyList<string> Routes { get; }
}

public interface ISecuritySubsystem : ISubsystem
{
    void Arm();
    void Disarm();
    bool IsArmed { get; }
}

public interface IEnergySubsystem : ISubsystem
{
    int CurrentUsageKw { get; }
    int AlertThresholdKw { get; }
}