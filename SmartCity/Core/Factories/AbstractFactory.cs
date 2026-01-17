namespace SmartCitySystem.Core.Factories
{
    public interface ISensor
    {
        string GetStatus();
        void CollectData();
    }
    public interface ICamera
    {
        void CaptureImage();
        string GetResolution();
    }
    public interface ILightBulb
    {
        void TurnOn();
    }

    public interface AbstractFactory
    {
        ISensor CreateTrafficSensor();
        ICamera CreateSecurityCamera();
        ILightBulb CreateLightBulb();
    }

    public class StandardTrafficSensor : ISensor
    {
        public string GetStatus() => "Trafik sensor: Faol (Standard rejim).";
        public void CollectData() => Console.WriteLine("[Standard Sensor] Tezlik va transport sonini yig'moqda.");
    }
    public class StandardCamera : ICamera
    {
        public void CaptureImage() => Console.WriteLine("[Standard Camera] 720p rasm olindi.");
        public string GetResolution() => "720p";
    }
    public class BasicLightBulb : ILightBulb
    {
        public void TurnOn() => Console.WriteLine("[Basic Chiroq] Yoqildi.");
    }

    public class HighDefinitionSensor : ISensor
    {
        public string GetStatus() => "HD Trafik sensor: Faol (Tahlil rejimida).";
        public void CollectData() => Console.WriteLine("[HD Sensor] Katta ma'lumotlar va tasvir tahlilini yig'moqda.");
    }
    public class UltraHDCamera : ICamera
    {
        public void CaptureImage() => Console.WriteLine("[UltraHD Camera] 4K rasm olindi.");
        public string GetResolution() => "4K";
    }
    public class LEDLightBulb : ILightBulb
    {
        public void TurnOn() => Console.WriteLine("[LED Chiroq] Yoqildi (Yuqori samaradorlik).");
    }

    public class StandardFactory : AbstractFactory
    {
        public ISensor CreateTrafficSensor() => new StandardTrafficSensor();
        public ICamera CreateSecurityCamera() => new StandardCamera();
        public ILightBulb CreateLightBulb() => new BasicLightBulb();
    }
    public class AdvancedFactory : AbstractFactory
    {
        public ISensor CreateTrafficSensor() => new HighDefinitionSensor();
        public ICamera CreateSecurityCamera() => new UltraHDCamera();
        public ILightBulb CreateLightBulb() => new LEDLightBulb();
    }
}