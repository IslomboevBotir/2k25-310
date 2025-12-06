// Factories/Flyweight.cs
namespace SmartCitySystem.Factories
{
    public class LightStyle
    {
        public string Intensity { get; set; } = "Normal";
        public string Color { get; set; } = "White";
    }

    public class LightStyleFactory
    {
        private readonly Dictionary<string, LightStyle> _styles = new();

        public LightStyle GetStyle(string intensity)
        {
            if (!_styles.ContainsKey(intensity))
                _styles[intensity] = new LightStyle { Intensity = intensity };

            return _styles[intensity];
        }
    }
}