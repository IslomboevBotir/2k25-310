namespace SmartCitySystem.Transport
{
    public class Bus : Vehicle
    {
        public override void Operate() => Console.WriteLine($"Bus {Id} yo‘lovchilarni olib ketmoqda.");
    }
}