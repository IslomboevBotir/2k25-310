namespace SmartCitySystem.Core.Singleton;

/// <summary>
/// Singleton Pattern: Central Smart City Controller
/// Ensures only one instance of the controller exists throughout the application
/// This is the main entry point for managing all city subsystems
/// </summary>
public sealed class SmartCityController
{
    private static SmartCityController? _instance;
    private static readonly object _lock = new object();
    private readonly List<ISubsystem> _subsystems;
    private bool _isRunning;

    /// <summary>
    /// Private constructor to prevent direct instantiation
    /// </summary>
    private SmartCityController()
    {
        _subsystems = new List<ISubsystem>();
        _isRunning = false;
    }

    /// <summary>
    /// Gets the singleton instance of the Smart City Controller
    /// Thread-safe implementation using double-check locking
    /// </summary>
    public static SmartCityController Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new SmartCityController();
                    }
                }
            }
            return _instance;
        }
    }

    /// <summary>
    /// Gets whether the controller is running
    /// </summary>
    public bool IsRunning => _isRunning;

    /// <summary>
    /// Gets the list of registered subsystems
    /// </summary>
    public IReadOnlyList<ISubsystem> Subsystems => _subsystems.AsReadOnly();

    /// <summary>
    /// Registers a subsystem with the controller
    /// </summary>
    public void RegisterSubsystem(ISubsystem subsystem)
    {
        if (!_subsystems.Contains(subsystem))
        {
            _subsystems.Add(subsystem);
            Console.WriteLine($"Subsystem '{subsystem.Name}' registered successfully.");
        }
    }

    /// <summary>
    /// Starts all registered subsystems
    /// </summary>
    public void StartAllSystems()
    {
        _isRunning = true;
        Console.WriteLine("\n=== Starting Smart City Systems ===");
        foreach (var subsystem in _subsystems)
        {
            subsystem.Start();
        }
        Console.WriteLine("All systems started successfully.\n");
    }

    /// <summary>
    /// Stops all registered subsystems
    /// </summary>
    public void StopAllSystems()
    {
        Console.WriteLine("\n=== Stopping Smart City Systems ===");
        foreach (var subsystem in _subsystems)
        {
            subsystem.Stop();
        }
        _isRunning = false;
        Console.WriteLine("All systems stopped.\n");
    }

    /// <summary>
    /// Gets the status of all subsystems
    /// </summary>
    public void DisplayAllStatus()
    {
        Console.WriteLine("\n=== Smart City System Status ===");
        foreach (var subsystem in _subsystems)
        {
            Console.WriteLine($"\n{subsystem.Name}:");
            Console.WriteLine(subsystem.GetStatus());
        }
        Console.WriteLine("================================\n");
    }
}
