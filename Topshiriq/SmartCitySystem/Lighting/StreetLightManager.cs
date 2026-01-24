
using SmartCitySystem.Factories;   

namespace SmartCitySystem.Lighting
{
    public interface ILightComponent
    {
        void PrintStatus(int indent = 0);
    }

    public class StreetLight : ILightComponent
    {
        public string Id { get; }
        private readonly LightStyle _style;
        private bool _isOn = false;

        public StreetLight(string id, LightStyle style)
        {
            Id = id;
            _style = style;
        }

        public void TurnOn() => _isOn = true;
        public void TurnOff() => _isOn = false;

        public void PrintStatus(int indent = 0)
        {
            string status = _isOn ? "YOQILGAN" : "O‘CHIQ";
            Console.WriteLine($"{new string(' ', indent)}Chiroq {Id} → {status} ({_style.Intensity})");
        }
    }

    public class LightZone : ILightComponent
    {
        public string Name { get; }
        private readonly List<ILightComponent> _children = new();

        public LightZone(string name) => Name = name;

        public void Add(ILightComponent component) => _children.Add(component);

        public void PrintStatus(int indent = 0)
        {
            Console.WriteLine($"{new string(' ', indent)}HUDUD: {Name}");
            foreach (var child in _children)
                child.PrintStatus(indent + 4);
        }
    }

    public class EnergySavingDecorator : ILightComponent
    {
        private readonly ILightComponent _component;

        public EnergySavingDecorator(ILightComponent component) => _component = component;

        public void PrintStatus(int indent = 0)
        {
            Console.WriteLine($"{new string(' ', indent)}[ENERGO TEJAMKOR REJIM]");
            _component.PrintStatus(indent + 4);
        }
    }

    public class LightingManager
    {
        public static readonly LightingManager Instance = new();
        private readonly LightStyleFactory _factory = new();
        private readonly List<ILightComponent> _zones = new();

        private LightingManager()
        {
            var downtown = new LightZone("Markaz");
            var dimmed = _factory.GetStyle("Dimmed");
            downtown.Add(new StreetLight("L-001", dimmed));
            downtown.Add(new StreetLight("L-002", dimmed));

            var suburb = new LightZone("Chekka hudud");
            var full = _factory.GetStyle("Full");
            suburb.Add(new StreetLight("L-101", full));

            _zones.Add(downtown);
            _zones.Add(new EnergySavingDecorator(suburb));
        }

        public void SetAllToMode(string mode)
        {
            Console.WriteLine($"[Yorug‘lik] Rejim: {mode}");
            _factory.GetStyle(mode); // faqat demo uchun
        }

        public void PrintStatus()
        {
            Console.WriteLine("\n=== YORUG‘LIK TIZIMI ===");
            foreach (var zone in _zones)
                zone.PrintStatus();
        }
    }
}