using SmartCity.Core;

namespace SmartCity.Factories;

public interface ISubsystemFactory
{
    ILightingSubsystem CreateLightingSubsystem(SmartCityConfiguration config);
    ITransportationSubsystem CreateTransportationSubsystem(SmartCityConfiguration config);
    ISecuritySubsystem CreateSecuritySubsystem(SmartCityConfiguration config);
    IEnergySubsystem CreateEnergySubsystem(SmartCityConfiguration config);
}