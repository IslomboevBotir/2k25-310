using System;

namespace Modules
{
    public class TransportSubsystem : ISubsystem
    {
        private bool _running = false;
        public string Status => _running ? "Transport Active" : "Transport Idle";

        public void Start()
        {
            _running = true;
            Console.WriteLine("Transport subsystem started: traffic optimization enabled.");
        }

        public void Stop()
        {
            _running = false;
            Console.WriteLine("Transport subsystem stopped.");
        }

        public void Toggle()
        {
            _running = !_running;
            Console.WriteLine($"Transport toggled: {(_running ? "Active" : "Idle")}");
        }
    }
}
