using SmartCitySystem.Transport;

namespace SmartCitySystem.Factories
{
    public abstract class TransportFactory
    {
        public abstract Vehicle CreateVehicle(string id);
    }

    public class BusFactory : TransportFactory
    {
        public override Vehicle CreateVehicle(string id) => new Bus { Id = id };
    }

    public class TramFactory : TransportFactory
    {
        public override Vehicle CreateVehicle(string id) => new Tram { Id = id };
    }
}