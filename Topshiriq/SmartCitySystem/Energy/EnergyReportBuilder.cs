
namespace SmartCitySystem.Energy
{
    public class EnergyReport
    {
        public DateTime Generated { get; set; }
        public double TotalConsumption { get; set; }
        public double Saved { get; set; }

        public override string ToString()
            => $"Hisobot {Generated:yyyy-MM-dd HH:mm} → Sarf: {TotalConsumption} kWh, Tejalgan: {Saved} kWh";
    }

    public interface IEnergyReportBuilder
    {
        void Reset();
        void SetConsumption();
        void SetSavings();
        EnergyReport GetReport();
    }

    public class MonthlyEnergyReportBuilder : IEnergyReportBuilder
    {
        private EnergyReport _report = new();

        public void Reset() => _report = new EnergyReport { Generated = DateTime.Now };
        public void SetConsumption() => _report.TotalConsumption = 18_750.5;
        public void SetSavings() => _report.Saved = 1_240.8;
        public EnergyReport GetReport() => _report;

        
        public MonthlyEnergyReportBuilder()
        {
            Reset();
            SetConsumption();
            SetSavings();
        }
    }
}