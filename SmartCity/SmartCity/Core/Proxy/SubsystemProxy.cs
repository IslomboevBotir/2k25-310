using Modules;
using System;


namespace Proxy
{
    public class SubsystemProxy : ISubsystem
    {
        private readonly ISubsystem _inner;
        private readonly string _role;

        public SubsystemProxy(ISubsystem inner, string role)
        {
            _inner = inner;
            _role = role;
        }

        public string Status => _inner.Status;

        public void Start()
        {
            if (_role == "admin") _inner.Start();
            else Console.WriteLine("[Proxy] Permission denied: Start");
        }

        public void Stop()
        {
            if (_role == "admin") _inner.Stop();
            else Console.WriteLine("[Proxy] Permission denied: Stop");
        }

        public void Toggle()
        {
            if (_role == "admin") _inner.Toggle();
            else Console.WriteLine("[Proxy] Permission denied: Toggle");
        }
    }
}
