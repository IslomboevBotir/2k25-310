using System.Collections.Generic;
using System.Linq;
using SmartCity.Core;

namespace SmartCity.Modules;

public sealed class TransportationSubsystem : ITransportationSubsystem
{
    private readonly List<string> _routes;

    public TransportationSubsystem(IEnumerable<string> routes)
    {
        _routes = routes?.Where(r => !string.IsNullOrWhiteSpace(r)).ToList() ?? new List<string>();
    }

    public IReadOnlyList<string> Routes => _routes.AsReadOnly();

    public void AddRoute(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return;
        }

        if (!_routes.Contains(name))
        {
            _routes.Add(name);
        }
    }

    public void RemoveRoute(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return;
        }

        _routes.Remove(name);
    }

    public string GetStatus()
    {
        var routes = _routes.Count == 0 ? "None" : string.Join(", ", _routes);
        return $"Transportation: Routes={routes}";
    }
}