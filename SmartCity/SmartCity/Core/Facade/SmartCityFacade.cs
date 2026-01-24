using System;
using System.Text;
using Modules;
using Core;


namespace Core
{
    public class SmartCityFacade
    {
        private readonly CentralController _controller;

        public SmartCityFacade(CentralController controller)
        {
            _controller = controller;
        }

        public void RegisterSubsystem(string name, ISubsystem subsystem)
        {
            _controller.Register(name, subsystem);
        }

        public void StartAll()
        {
            Console.WriteLine("[Facade] Starting all subsystems...");
            foreach (var kv in _controller.GetAll())
                kv.Value.Start();
        }

        public void StopAll()
        {
            Console.WriteLine("[Facade] Stopping all subsystems...");
            foreach (var kv in _controller.GetAll())
                kv.Value.Stop();
        }

        public void ShowStatus()
        {
            Console.WriteLine("[Facade] Status of subsystems:");
            foreach (var kv in _controller.GetAll())
                Console.WriteLine($"{kv.Key}: {kv.Value.Status}");
        }

        public void ExecuteOnSubsystem(string name, Action<ISubsystem> action)
        {
            var s = _controller.GetSubsystem(name);
            if (s == null) { Console.WriteLine("Subsystem not found."); return; }
            action(s);
        }

        public string GetStatusSummary()
        {
            var sb = new StringBuilder();
            foreach (var kv in _controller.GetAll())
                sb.AppendLine($"{kv.Key}: {kv.Value.Status}");
            return sb.ToString();
        }
    }
}
