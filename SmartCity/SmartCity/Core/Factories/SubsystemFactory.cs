using Modules;
using System;

namespace Factories
{
    public class SubsystemFactory
    {
        public ISubsystem CreateSubsystem(string type)
        {
            switch (type.ToLower())
            {
                case "lighting": return new LightingSubsystem();
                case "transport": return new TransportSubsystem();
                case "security": return new SecuritySubsystem();
                case "energy": return new EnergySubsystem();
                default:
                    throw new ArgumentException($"Unknown subsystem type: {type}");
            }
        }
    }
}
