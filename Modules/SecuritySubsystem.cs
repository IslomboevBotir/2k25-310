using SmartCity.Core;

namespace SmartCity.Modules;

public sealed class SecuritySubsystem : ISecuritySubsystem
{
    public SecuritySubsystem(bool armed)
    {
        IsArmed = armed;
    }

    public bool IsArmed { get; private set; }

    public void Arm()
    {
        IsArmed = true;
    }

    public void Disarm()
    {
        IsArmed = false;
    }

    public string GetStatus()
    {
        var state = IsArmed ? "Armed" : "Disarmed";
        return $"Security: Status={state}";
    }
}