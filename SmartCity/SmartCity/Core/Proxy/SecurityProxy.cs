namespace SmartCitySystem.Core.Proxy
{
    using SmartCitySystem.Modules.Security;

    // Proxy sinfi: Real Subject'ga kirishdan oldin nazorat qiladi.
    public class SecurityProxy : ISecurityAccess
    {
        private readonly SecuritySystem _realSubject = new SecuritySystem();
        private readonly string _userRole;

        public SecurityProxy(string userRole)
        {
            _userRole = userRole;
            Console.WriteLine($"[Proxy] Yangi kirish: {_userRole} rolida o'rnatildi.");
        }

        private bool CheckAccess(string requiredRole)
        {
            if (_userRole == requiredRole)
            {
                return true;
            }
            Console.WriteLine($"[Proxy VETO] Ogohlantirish: `{_userRole}` uchun `{requiredRole}` ruxsati talab qilinadi. Rad etildi!");
            return false;
        }

        public void AccessCameraFeeds()
        {
            if (CheckAccess("Operator"))
            {
                _realSubject.AccessCameraFeeds(); // Ruxsat berilsa, haqiqiy ob'ekt chaqiriladi
            }
        }

        public void ResetAlarmSystem()
        {
            if (CheckAccess("Administrator"))
            {
                _realSubject.ResetAlarmSystem(); // Ruxsat berilsa, haqiqiy ob'ekt chaqiriladi
            }
        }
    }
}