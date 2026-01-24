namespace SmartCitySystem.Modules.Security
{
    public interface ISecurityAccess
    {
        void AccessCameraFeeds();
        void ResetAlarmSystem();
    }

    public class SecuritySystem : ISecurityAccess
    {
        public void AccessCameraFeeds() => Console.WriteLine("[Security] MAXFIY: Barcha kamera yozuvlariga kirish ruxsati berildi.");
        public void ResetAlarmSystem() => Console.WriteLine("[Security] MAXFIY: Trevoga tizimi qayta tiklandi.");
    }
}