
namespace SmartCitySystem.Patterns
{
    public class SignalConfig : ICloneable
    {
        public int GreenSeconds { get; set; }
        public int RedSeconds { get; set; }

        public object Clone() => this.MemberwiseClone();
    }
}