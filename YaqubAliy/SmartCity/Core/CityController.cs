using SmartCity.Modules.Lighting;
using SmartCity.Modules.Transport;
using SmartCity.Modules.Security;
using SmartCity.Modules.Energy;
using SmartCity.Core.Factories;
using SmartCity.Core.Proxy;

namespace SmartCity.Core
{
    /// <summary>
    /// SINGLETON PATTERN + FACADE PATTERN
    /// Central controller that provides a unified interface to all city subsystems
    /// Ensures only one instance manages the entire city
    /// </summary>
    public sealed class CityController
    {
        private static CityController? _instance;
        private static readonly object _lock = new object();

        private LightingSystem? _lightingSystem;
        private ITransportSystem? _transportSystem;
        private SecuritySystemProxy? _securityProxy;
        private EnergyMonitor? _energyMonitor;

        // SINGLETON: Private constructor
        private CityController() { }

        // SINGLETON: Thread-safe instance access
        public static CityController Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new CityController();
                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// FACADE PATTERN: Simplified initialization of complex subsystems
        /// </summary>
        public void InitializeCity()
        {
            Console.WriteLine("🏙️  Initializing Smart City Systems...\n");

            // FACTORY PATTERN: Create lighting system
            var lightingFactory = new LightingFactory();
            _lightingSystem = lightingFactory.CreateLightingSystem("advanced");

            // FACTORY PATTERN: Create transport system
            var transportFactory = new TransportFactory();
            _transportSystem = transportFactory.CreateTransportSystem("smart");

            // PROXY PATTERN: Security system with access control
            _securityProxy = new SecuritySystemProxy("admin");

            // Create energy monitor
            _energyMonitor = new EnergyMonitor();

            Console.WriteLine("✅ All systems initialized successfully!\n");
        }

        /// <summary>
        /// FACADE: Simplified lighting control interface
        /// </summary>
        public void ControlLighting(string mode, int brightness)
        {
            Console.WriteLine($"\n💡 Controlling lighting: {mode} (Brightness: {brightness}%)");
            _lightingSystem?.AdjustBrightness(brightness);
            _lightingSystem?.SetMode(mode);
            _energyMonitor?.RecordConsumption("Lighting", brightness * 0.5);
        }

        /// <summary>
        /// FACADE: Simplified traffic management
        /// </summary>
        public void ManageTraffic(string mode)
        {
            Console.WriteLine($"\n🚦 Managing traffic: {mode}");
            _transportSystem?.OptimizeTraffic();
            if (mode == "EMERGENCY")
            {
                _transportSystem?.EnableEmergencyMode();
            }
        }

        /// <summary>
        /// FACADE: Security status check (through Proxy)
        /// </summary>
        public void CheckSecurityStatus()
        {
            Console.WriteLine("\n🔒 Checking security status...");
            _securityProxy?.GetStatus();
            _securityProxy?.MonitorCameras();
        }

        /// <summary>
        /// FACADE: Energy monitoring
        /// </summary>
        public void MonitorEnergy()
        {
            Console.WriteLine("\n⚡ Energy Monitoring Report:");
            _energyMonitor?.GenerateReport();
        }

        /// <summary>
        /// FACADE: Emergency coordination
        /// </summary>
        public void TriggerEmergency(string message)
        {
            Console.WriteLine($"\n🚨 EMERGENCY ALERT: {message}");
            _securityProxy?.TriggerAlarm();
            _transportSystem?.EnableEmergencyMode();
            _lightingSystem?.SetMode("EMERGENCY");
        }

        /// <summary>
        /// FACADE: Comprehensive city status
        /// </summary>
        public void GenerateCityReport()
        {
            Console.WriteLine("\n" + new string('═', 50));
            Console.WriteLine("📊 SMART CITY STATUS REPORT");
            Console.WriteLine(new string('═', 50));

            _lightingSystem?.GetStatus();
            _transportSystem?.GetSystemInfo();
            _securityProxy?.GetStatus();
            _energyMonitor?.GenerateReport();

            Console.WriteLine(new string('═', 50));
        }
    }
}