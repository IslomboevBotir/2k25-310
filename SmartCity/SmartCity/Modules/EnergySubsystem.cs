using System;

namespace Modules
{
    public class EnergySubsystem : ISubsystem
    {
        private bool _saving = false;
        public string Status => _saving ? "Energy Saving Mode" : "Normal Energy Mode";

        public void Start()
        {
            _saving = true;
            Console.WriteLine("Energy subsystem: saving mode enabled.");
        }

        public void Stop()
        {
            _saving = false;
            Console.WriteLine("Energy subsystem: saving mode disabled.");
        }

        public void Toggle()
        {
            _saving = !_saving;
            Console.WriteLine($"Energy toggled: {(_saving ? "Saving" : "Normal")}");
        }
    }
}
