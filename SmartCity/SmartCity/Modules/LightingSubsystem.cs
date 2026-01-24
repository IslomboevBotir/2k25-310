using System;

namespace Modules
{
    public class LightingSubsystem : ISubsystem
    {
        private bool _on = false;
        public string Status => _on ? "Lighting ON" : "Lighting OFF";

        public void Start()
        {
            _on = true;
            Console.WriteLine("Lighting subsystem started.");
        }

        public void Stop()
        {
            _on = false;
            Console.WriteLine("Lighting subsystem stopped.");
        }

        public void Toggle()
        {
            _on = !_on;
            Console.WriteLine($"Lighting toggled to: {_on}");
        }
    }
}
