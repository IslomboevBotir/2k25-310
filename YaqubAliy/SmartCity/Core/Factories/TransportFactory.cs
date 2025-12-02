using SmartCity.Modules.Transport;

namespace SmartCity.Core.Factories
{
    /// <summary>
    /// FACTORY METHOD PATTERN
    /// Creates different transport management systems
    /// </summary>
    public class TransportFactory
    {
        public ITransportSystem CreateTransportSystem(string type)
        {
            Console.WriteLine($"🏭 Factory creating {type} transport system...");

            return type.ToLower() switch
            {
                "basic" => new BasicTransportSystem(),
                "smart" => new SmartTransportSystem(),
                _ => new BasicTransportSystem()
            };
        }
    }
}