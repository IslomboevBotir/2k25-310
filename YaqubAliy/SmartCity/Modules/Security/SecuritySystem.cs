namespace SmartCity.Modules.Security
{
    /// <summary>
    /// Real security system implementation
    /// Protected by SecuritySystemProxy
    /// </summary>
    public class SecuritySystem
    {
        private bool _alarmActive;
        private int _camerasOnline;

        public SecuritySystem()
        {
            _alarmActive = false;
            _camerasOnline = new Random().Next(50, 100);
        }

        public void CheckStatus()
        {
            Console.WriteLine($"\n🔒 Security Status:");
            Console.WriteLine($"   Alarm: {(_alarmActive ? "🔴 ACTIVE" : "🟢 Inactive")}");
            Console.WriteLine($"   Cameras Online: {_camerasOnline}/100");
            Console.WriteLine($"   Threat Level: {(_alarmActive ? "HIGH" : "LOW")}");
        }

        public void MonitorCameras()
        {
            Console.WriteLine($"📹 Monitoring {_camerasOnline} security cameras...");
            Console.WriteLine("   ✓ All zones covered");
            Console.WriteLine("   ✓ No suspicious activity detected");
        }

        public void ActivateAlarm()
        {
            _alarmActive = true;
            Console.WriteLine("🚨 ALARM ACTIVATED! All security personnel notified!");
        }
    }
}