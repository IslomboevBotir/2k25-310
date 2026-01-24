namespace SmartCitySystem.Transport
{
    public abstract class Vehicle
    {
        public string Id { get; set; } = "";
        public abstract void Operate();
    }
}