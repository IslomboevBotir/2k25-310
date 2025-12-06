
using SmartCitySystem.Energy;

namespace SmartCitySystem.Energy
{
    public class EnergyManager
    {
        private bool _savingMode = false;

        public void EnableSavingMode()
        {
            _savingMode = true;
            Console.WriteLine("[Energiya] Tejamkor rejim YOQILDI");
        }

        public void DisableSavingMode()
        {
            _savingMode = false;
            Console.WriteLine("[Energiya] Tejamkor rejim O‘CHIRILDI");
        }

        public EnergyReport BuildReport(MonthlyEnergyReportBuilder builder)
        {
            return builder.GetReport();
        }

        public void PrintStatus()
            => Console.WriteLine($"Energiya → Tejamkor rejim: {_savingMode}");
    }
}