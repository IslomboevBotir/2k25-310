using System.Collections.Generic;
using SmartCity.Modules;

namespace SmartCity.Core;

public sealed class SmartCityConfiguration
{
    public string CityName { get; set; } = string.Empty;
    public LightingMode InitialLightingMode { get; set; }
    public IReadOnlyList<string> InitialRoutes { get; set; } = new List<string>();
    public int EnergyAlertThresholdKw { get; set; }
    public bool SecurityArmed { get; set; }
    public IReadOnlyList<string> AllowedSecurityRoles { get; set; } = new List<string>();
}
