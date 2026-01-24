using SmartCitySystem.Factories;

namespace SmartCitySystem.Transport
{
    public class TransportManager
    {
        private readonly List<Vehicle> _vehicles = new();
        private int _speedLimit = 50;

        public void AddVehicle(TransportFactory factory, string id)
        {
            var vehicle = factory.CreateVehicle(id);
            _vehicles.Add(vehicle);
            Console.WriteLine($"[Transport] Qo‘shildi: {id} ({vehicle.GetType().Name})");
        }

        public void OperateAll() => _vehicles.ForEach(v => v.Operate());

        public void SetSpeedLimit(int limit)
        {
            _speedLimit = limit;
            Console.WriteLine($"[Transport] Tezlik cheklovi: {limit} km/soat");
        }

        public void PrintStatus() => 
            Console.WriteLine($"Transport: avtomobillar={_vehicles.Count}, limit={_speedLimit} km/soat");
    }
}