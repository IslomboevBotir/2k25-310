using System;
using System.Collections.Generic;
using SmartCity.Core;

namespace SmartCity.Proxies;

public interface IUserContext
{
    string Role { get; }
}

public sealed class UserContext : IUserContext
{
    public UserContext(string role)
    {
        Role = string.IsNullOrWhiteSpace(role) ? "Guest" : role;
    }

    public string Role { get; }
}

public interface IAccessPolicy
{
    bool CanAccessSecurity(string role);
}

public sealed class RoleBasedAccessPolicy : IAccessPolicy
{
    private readonly HashSet<string> _allowedRoles;

    public RoleBasedAccessPolicy(IEnumerable<string> allowedRoles)
    {
        _allowedRoles = new HashSet<string>(allowedRoles ?? Array.Empty<string>(), StringComparer.OrdinalIgnoreCase);
    }

    public bool CanAccessSecurity(string role)
    {
        return _allowedRoles.Contains(role ?? string.Empty);
    }
}


public sealed class SecurityProxy : ISecuritySubsystem
{
    private readonly ISecuritySubsystem _inner;
    private readonly IAccessPolicy _policy;
    private readonly IUserContext _userContext;

    public SecurityProxy(ISecuritySubsystem inner, IAccessPolicy policy, IUserContext userContext)
    {
        _inner = inner;
        _policy = policy;
        _userContext = userContext;
    }

    public bool IsArmed => _inner.IsArmed;

    public void Arm()
    {
        EnsureAccess();
        _inner.Arm();
    }

    public void Disarm()
    {
        EnsureAccess();
        _inner.Disarm();
    }

    public string GetStatus()
    {
        return _inner.GetStatus();
    }

    private void EnsureAccess()
    {
        if (!_policy.CanAccessSecurity(_userContext.Role))
        {
            throw new UnauthorizedAccessException("Role is not permitted to manage security.");
        }
    }
}
