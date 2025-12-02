namespace SmartCity.Modules.Energy
{
    /// <summary>
    /// COMPOSITE-LIKE PATTERN
    /// Monitors energy consumption across different city subsystems
    /// Aggregates data from multiple sources
    /// </summary>
    public class EnergyMonitor
    {
        private readonly Dictionary<string, double> _consumption;

        public EnergyMonitor()
        {
            _consumption = new Dictionary<string, double>
            {
                { "Lighting", 0 },
                { "Transport", 0 },
                { "Security", 15.5 },
                { "Other", 8.3 }
            };
        }

        public void RecordConsumption(string subsystem, double kwh)
        {
            if (_consumption.ContainsKey(subsystem))
            {
                _consumption[subsystem] += kwh;
            }
        }

        public void GenerateReport()
        {
            Console.WriteLine("\n⚡ Energy Consumption Report:");
            double total = 0;

            foreach (var item in _consumption)
            {
                Console.WriteLine($"   {item.Key,-15}: {item.Value:F2} kWh");
                total += item.Value;
            }

            Console.WriteLine($"   {new string('-', 30)}");
            Console.WriteLine($"   {"TOTAL",-15}: {total:F2} kWh");
            Console.WriteLine($"   Efficiency Rating: {(total < 100 ? "⭐⭐⭐ Excellent" : "⭐⭐ Good")}");
        }
    }
}