namespace SmartCity.Modules.Transport
{
    /// <summary>
    /// Interface for transport systems
    /// </summary>
    public interface ITransportSystem
    {
        void OptimizeTraffic();
        void EnableEmergencyMode();
        void GetSystemInfo();
    }

    /// <summary>
    /// Basic traffic management
    /// </summary>
    public class BasicTransportSystem : ITransportSystem
    {
        public void OptimizeTraffic()
        {
            Console.WriteLine("🚦 Basic traffic optimization: Adjusting traffic light timings");
        }

        public void EnableEmergencyMode()
        {
            Console.WriteLine("🚨 Emergency mode: Clearing main routes");
        }

        public void GetSystemInfo()
        {
            Console.WriteLine("\n🚗 Transport: Basic Traffic Management System");
            Console.WriteLine("   Features: Traffic light control, Basic routing");
        }
    }

    /// <summary>
    /// Advanced smart transport with AI optimization
    /// </summary>
    public class SmartTransportSystem : ITransportSystem
    {
        private int _vehicleCount = 0;

        public void OptimizeTraffic()
        {
            _vehicleCount = new Random().Next(100, 500);
            Console.WriteLine($"🚦 AI-powered optimization: Analyzing {_vehicleCount} vehicles");
            Console.WriteLine("   ✓ Dynamic lane allocation activated");
            Console.WriteLine("   ✓ Predictive traffic flow enabled");
        }

        public void EnableEmergencyMode()
        {
            Console.WriteLine("🚨 Smart Emergency Response:");
            Console.WriteLine("   ✓ Emergency vehicle route cleared");
            Console.WriteLine("   ✓ Traffic signals synchronized");
            Console.WriteLine("   ✓ Alternative routes suggested to drivers");
        }

        public void GetSystemInfo()
        {
            Console.WriteLine("\n🚗 Transport: Smart AI Traffic Management");
            Console.WriteLine($"   Active vehicles: {_vehicleCount}");
            Console.WriteLine("   Features: AI optimization, Emergency routing, Real-time updates");
        }
    }
}