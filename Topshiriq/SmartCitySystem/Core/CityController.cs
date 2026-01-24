using SmartCitySystem.Transport;
using SmartCitySystem.Lighting;
using SmartCitySystem.Security;
using SmartCitySystem.Energy;

namespace SmartCitySystem.Core
{
    public sealed class CityController
    {
        private static readonly Lazy<CityController> _instance = new(() => new CityController());
        public static CityController Instance => _instance.Value;

        public TransportManager Transport { get; }
        public LightingManager Lighting { get; }
        public SecurityManager Security { get; }
        public EnergyManager Energy { get; }

        private CityController()
{
    Transport = new TransportManager();
    Lighting = LightingManager.Instance;   
    Security = new SecurityManager();
    Energy = new EnergyManager();
}
        public void EveningMode()
        {
            Console.WriteLine("\nKECHKI REJIM");
            Lighting.SetAllToMode("Dimmed");
            Transport.SetSpeedLimit(40);
            Energy.EnableSavingMode();
            Security.SetAlertLevel(AlertLevel.Medium);
        }

        public void EmergencyMode()
        {
            Console.WriteLine("\nFAVQULODDA HOLAT!");
            Lighting.SetAllToMode("Full");
            Transport.SetSpeedLimit(20);
            Security.SetAlertLevel(AlertLevel.High);
            Energy.DisableSavingMode();
        }
       

       
        public void PrintStatus()
        {
            Console.WriteLine("\nSHAHAR HOLATI");
            Lighting.PrintStatus();
            Transport.PrintStatus();
            Security.PrintStatus();
            Energy.PrintStatus();
        }
    }
}