namespace SmartCitySystem.Core.Proxy;

/// <summary>
/// Interface for secure subsystems
/// </summary>
public interface ISecureSubsystem
{
    string AccessData(string userId);
    void ModifySettings(string userId, string setting);
}

/// <summary>
/// Real Subject: Actual security subsystem with sensitive operations
/// </summary>
public class RealSecuritySubsystem : ISecureSubsystem
{
    public string AccessData(string userId)
    {
        return $"[REAL SYSTEM] Sensitive security data accessed by {userId}";
    }

    public void ModifySettings(string userId, string setting)
    {
        Console.WriteLine($"[REAL SYSTEM] Settings modified by {userId}: {setting}");
    }
}

/// <summary>
/// Proxy Pattern: Controls access to the real security subsystem
/// Provides access control and logging for sensitive operations
/// </summary>
public class SecuritySubsystemProxy : ISecureSubsystem
{
    private readonly RealSecuritySubsystem _realSubsystem;
    private readonly HashSet<string> _authorizedUsers;
    private readonly List<string> _accessLog;

    public SecuritySubsystemProxy()
    {
        _realSubsystem = new RealSecuritySubsystem();
        _authorizedUsers = new HashSet<string> { "admin", "security_chief", "operator" };
        _accessLog = new List<string>();
    }

    /// <summary>
    /// Adds an authorized user
    /// </summary>
    public void AddAuthorizedUser(string userId)
    {
        _authorizedUsers.Add(userId);
        LogAccess($"User {userId} added to authorized list");
    }

    /// <summary>
    /// Checks if user is authorized
    /// </summary>
    private bool IsAuthorized(string userId)
    {
        return _authorizedUsers.Contains(userId);
    }

    /// <summary>
    /// Logs access attempts
    /// </summary>
    private void LogAccess(string message)
    {
        var logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";
        _accessLog.Add(logEntry);
        Console.WriteLine($"[PROXY LOG] {message}");
    }

    public string AccessData(string userId)
    {
        LogAccess($"Data access attempt by user: {userId}");
        
        if (IsAuthorized(userId))
        {
            LogAccess($"Access GRANTED for user: {userId}");
            return _realSubsystem.AccessData(userId);
        }
        else
        {
            LogAccess($"Access DENIED for user: {userId}");
            return $"[PROXY] Access denied for user {userId}. Insufficient privileges.";
        }
    }

    public void ModifySettings(string userId, string setting)
    {
        LogAccess($"Settings modification attempt by user: {userId}");
        
        if (IsAuthorized(userId))
        {
            LogAccess($"Modification GRANTED for user: {userId}");
            _realSubsystem.ModifySettings(userId, setting);
        }
        else
        {
            LogAccess($"Modification DENIED for user: {userId}");
            Console.WriteLine($"[PROXY] Settings modification denied for user {userId}");
        }
    }

    /// <summary>
    /// Gets the access log
    /// </summary>
    public IReadOnlyList<string> GetAccessLog()
    {
        return _accessLog.AsReadOnly();
    }

    /// <summary>
    /// Displays the access log
    /// </summary>
    public void DisplayAccessLog()
    {
        Console.WriteLine("\n=== Security Access Log ===");
        foreach (var entry in _accessLog)
        {
            Console.WriteLine(entry);
        }
        Console.WriteLine("===========================\n");
    }
}
