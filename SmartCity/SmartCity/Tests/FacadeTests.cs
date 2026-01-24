using Xunit;
using Core;
using Modules;

namespace SmartCity.Tests
{
    public class FacadeTests
    {
        [Fact]
        public void Facade_ShouldStartAllSubsystems()
        {
            var controller = CentralController.Instance;
            var facade = new SmartCityFacade(controller);

            var lighting = new LightingSubsystem();
            facade.RegisterSubsystem("lighting", lighting);

            facade.StartAll();

            Assert.Equal("Lighting ON", lighting.Status);
        }
    }
}
