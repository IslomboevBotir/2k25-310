using Xunit;
using Modules;
using Proxy;

namespace SmartCity.Tests
{
    public class ProxyTests
    {
        [Fact]
        public void Guest_ShouldNotToggleSubsystem()
        {
            var lighting = new LightingSubsystem();
            var proxy = new SubsystemProxy(lighting, "guest");

            proxy.Toggle();

            Assert.Equal("Lighting OFF", lighting.Status);
        }

        [Fact]
        public void Admin_ShouldToggleSubsystem()
        {
            var lighting = new LightingSubsystem();
            var proxy = new SubsystemProxy(lighting, "admin");

            proxy.Toggle();

            Assert.Equal("Lighting ON", lighting.Status);
        }
    }
}
