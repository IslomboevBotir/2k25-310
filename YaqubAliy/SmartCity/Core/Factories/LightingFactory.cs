using SmartCity.Modules.Lighting;

namespace SmartCity.Core.Factories
{
    /// <summary>
    /// FACTORY METHOD PATTERN
    /// Creates different types of lighting systems based on requirements
    /// </summary>
    public class LightingFactory
    {
        public LightingSystem CreateLightingSystem(string type)
        {
            Console.WriteLine($"🏭 Factory creating {type} lighting system...");

            return type.ToLower() switch
            {
                "basic" => new LightingSystem("Basic LED System", 50),
                "advanced" => new LightingSystem("Advanced Smart Lighting", 80),
                "premium" => new LightingSystem("Premium IoT Lighting", 100),
                _ => new LightingSystem("Standard System", 60)
            };
        }
    }
}