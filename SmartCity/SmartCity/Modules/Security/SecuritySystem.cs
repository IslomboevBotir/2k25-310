namespace SmartCitySystem.Modules.Security
{
    public interface ISecurityAccess
    {
        void AccessCameraFeeds();
        void ResetAlarmSystem();
    }

    // Haqiqiy Ob'ekt (Nihoyatda muhim va himoyalanishi kerak)
    public class SecuritySystem : ISecurityAccess
    {
        public void AccessCameraFeeds() => Console.WriteLine("[Security] MAXFIY: Barcha kamera yozuvlariga kirish ruxsati berildi.");
        public void ResetAlarmSystem() => Console.WriteLine("[Security] MAXFIY: Trevoga tizimi qayta tiklandi.");
    }
}