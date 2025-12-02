namespace SmartCity.Modules.Lighting
{
    /// <summary>
    /// Street lighting management system
    /// Supports brightness control and different modes
    /// </summary>
    public class LightingSystem
    {
        private string _systemName;
        private int _brightness;
        private string _mode;
        private readonly int _maxBrightness;

        public LightingSystem(string name, int maxBrightness)
        {
            _systemName = name;
            _maxBrightness = maxBrightness;
            _brightness = 0;
            _mode = "OFF";
        }

        public void AdjustBrightness(int level)
        {
            _brightness = Math.Min(level, _maxBrightness);
            Console.WriteLine($"💡 {_systemName}: Brightness set to {_brightness}%");
        }

        public void SetMode(string mode)
        {
            _mode = mode;
            Console.WriteLine($"💡 {_systemName}: Mode changed to {_mode}");

            switch (mode.ToUpper())
            {
                case "ON":
                    AdjustBrightness(100);
                    break;
                case "OFF":
                    AdjustBrightness(0);
                    break;
                case "EMERGENCY":
                    AdjustBrightness(100);
                    Console.WriteLine("🚨 Emergency lighting activated!");
                    break;
            }
        }

        public void GetStatus()
        {
            Console.WriteLine($"\n💡 Lighting System: {_systemName}");
            Console.WriteLine($"   Mode: {_mode} | Brightness: {_brightness}% | Max: {_maxBrightness}%");
        }
    }
}