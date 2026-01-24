using SmartCitySystem.Security;

namespace SmartCitySystem.Proxy
{
    public class SecurityProxy : ISecurity
    {
        private readonly ISecurity _real;
        private readonly string _role;

        public SecurityProxy(ISecurity real, string role) => (_real, _role) = (real, role);

        public void Arm() => IfAllowed("admin", () => _real.Arm());
        public void Disarm() => IfAllowed("admin", () => _real.Disarm());
        public void SetAlertLevel(AlertLevel level) => IfAllowed(["admin", "operator"], () => _real.SetAlertLevel(level));
        public void PrintStatus() => _real.PrintStatus();

        private void IfAllowed(string role, Action action) => IfAllowed([role], action);
        private void IfAllowed(string[] roles, Action action)
        {
            if (roles.Contains(_role)) action();
            else Console.WriteLine($"[Proxy] Ruxsat yo‘q → {_role}");
        }
    }
}