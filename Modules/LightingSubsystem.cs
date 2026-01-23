using SmartCity.Core;

namespace SmartCity.Modules;

public sealed class LightingSubsystem : ILightingSubsystem
{
    public LightingSubsystem(LightingMode initialMode)
    {
        CurrentMode = initialMode;
    }

    public LightingMode CurrentMode { get; private set; }

    public void SetMode(LightingMode mode)
    {
        CurrentMode = mode;
    }

    public string GetStatus()
    {
        return $"Lighting: Mode={CurrentMode}";
    }
}