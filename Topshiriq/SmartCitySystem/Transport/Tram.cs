namespace SmartCitySystem.Transport
{
    public class Tram : Vehicle
    {
        public override void Operate() => Console.WriteLine($"Tram {Id} rels bo‘ylab harakatlanmoqda.");
    }
}