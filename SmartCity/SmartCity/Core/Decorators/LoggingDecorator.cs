using Modules;
using System;

namespace Decorators
{
    public class LoggingDecorator : ISubsystem
    {
        private readonly ISubsystem _inner;

        public LoggingDecorator(ISubsystem inner)
        {
            _inner = inner;
        }

        public string Status => _inner.Status;

        public void Start()
        {
            Console.WriteLine("[Log] Starting subsystem...");
            _inner.Start();
            Console.WriteLine("[Log] Started.");
        }

        public void Stop()
        {
            Console.WriteLine("[Log] Stopping subsystem...");
            _inner.Stop();
            Console.WriteLine("[Log] Stopped.");
        }

        public void Toggle()
        {
            Console.WriteLine("[Log] Toggling subsystem...");
            _inner.Toggle();
            Console.WriteLine("[Log] Toggled.");
        }
    }
}
