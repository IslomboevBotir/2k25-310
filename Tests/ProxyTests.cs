using SmartCity.Core;
using SmartCity.Modules;
using SmartCity.Proxies;
using Xunit;

namespace SmartCity.Tests;

public sealed class ProxyTests
{
    [Fact]
    public void Proxy_Denies_Unauthorized_Access()
    {
        var security = new SecuritySubsystem(false);
        var policy = new RoleBasedAccessPolicy(new[] { "Admin" });
        var context = new UserContext("Guest");
        var proxy = new SecurityProxy(security, policy, context);

        Assert.Throws<UnauthorizedAccessException>(() => proxy.Arm());
    }

    [Fact]
    public void Proxy_Allows_Authorized_Access()
    {
        var security = new SecuritySubsystem(false);
        var policy = new RoleBasedAccessPolicy(new[] { "Admin" });
        var context = new UserContext("Admin");
        var proxy = new SecurityProxy(security, policy, context);

        proxy.Arm();

        Assert.True(security.IsArmed);
    }
}