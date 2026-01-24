using System;

namespace Modules
{
    public class SecuritySubsystem : ISubsystem
    {
        private bool _armed = false;
        public string Status => _armed ? "Security Armed" : "Security Disarmed";

        public void Start()
        {
            _armed = true;
            Console.WriteLine("Security subsystem: sensors armed.");
        }

        public void Stop()
        {
            _armed = false;
            Console.WriteLine("Security subsystem: sensors disarmed.");
        }

        public void Toggle()
        {
            _armed = !_armed;
            Console.WriteLine($"Security toggled: {(_armed ? "Armed" : "Disarmed")}");
        }
    }
}
