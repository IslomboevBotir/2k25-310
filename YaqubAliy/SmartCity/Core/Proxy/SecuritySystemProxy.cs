using SmartCity.Modules.Security;

namespace SmartCity.Core.Proxy
{
    /// <summary>
    /// PROXY PATTERN
    /// Controls access to the real SecuritySystem based on authentication
    /// Adds logging and access control
    /// </summary>
    public class SecuritySystemProxy
    {
        private SecuritySystem? _realSecuritySystem;
        private readonly string _userRole;
        private bool _authenticated;

        public SecuritySystemProxy(string userRole)
        {
            _userRole = userRole;
            _authenticated = Authenticate(userRole);
        }

        private bool Authenticate(string role)
        {
            Console.WriteLine($"🔐 Authenticating user with role: {role}");

            // Simple authentication logic
            if (role == "admin" || role == "security")
            {
                Console.WriteLine("✅ Authentication successful!");
                return true;
            }

            Console.WriteLine("❌ Authentication failed!");
            return false;
        }

        private SecuritySystem GetRealSystem()
        {
            if (_realSecuritySystem == null)
            {
                _realSecuritySystem = new SecuritySystem();
            }
            return _realSecuritySystem;
        }

        public void GetStatus()
        {
            if (_authenticated)
            {
                Console.WriteLine($"[PROXY] Access granted for {_userRole}");
                GetRealSystem().CheckStatus();
            }
            else
            {
                Console.WriteLine("❌ [PROXY] Access denied! Insufficient permissions.");
            }
        }

        public void MonitorCameras()
        {
            if (_authenticated)
            {
                Console.WriteLine($"[PROXY] Camera access granted for {_userRole}");
                GetRealSystem().MonitorCameras();
            }
            else
            {
                Console.WriteLine("❌ [PROXY] Camera access denied!");
            }
        }

        public void TriggerAlarm()
        {
            if (_authenticated)
            {
                Console.WriteLine($"[PROXY] Alarm trigger authorized by {_userRole}");
                GetRealSystem().ActivateAlarm();
            }
            else
            {
                Console.WriteLine("❌ [PROXY] Cannot trigger alarm - unauthorized!");
            }
        }
    }
}