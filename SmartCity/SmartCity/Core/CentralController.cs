using System;
using System.Collections.Generic;
using Modules;


namespace Core
{
    public class CentralController
    {
        private static readonly Lazy<CentralController> _instance = new Lazy<CentralController>(() => new CentralController());
        public static CentralController Instance => _instance.Value;

        private readonly Dictionary<string, ISubsystem> _registry = new Dictionary<string, ISubsystem>();

        private CentralController() { }

        public void Register(string name, ISubsystem subsystem)
        {
            _registry[name] = subsystem;
            Console.WriteLine($"[Controller] Registered subsystem: {name}");
        }

        public ISubsystem GetSubsystem(string name)
        {
            return _registry.ContainsKey(name) ? _registry[name] : null;
        }

        public IEnumerable<KeyValuePair<string, ISubsystem>> GetAll() => _registry;
    }
}
