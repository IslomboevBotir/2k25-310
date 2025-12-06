namespace SmartCitySystem.Security
{
    public enum AlertLevel { Low, Medium, High }

    public interface ISecurity
    {
        void Arm();
        void Disarm();
        void SetAlertLevel(AlertLevel level);
        void PrintStatus();
    }

    public class SecurityManager : ISecurity
    {
        private AlertLevel level = AlertLevel.Low;
        private bool armed;

        public void Arm() { armed = true; Console.WriteLine("[Xavfsizlik] Qurollandi"); }
        public void Disarm() { armed = false; Console.WriteLine("[Xavfsizlik] Qurol olib tashlandi"); }
        public void SetAlertLevel(AlertLevel l) { level = l; Console.WriteLine($"Xavf: {l}"); }
        public void PrintStatus() => Console.WriteLine($"Xavfsizlik: qurollangan={armed}, daraja={level}");
    }
}