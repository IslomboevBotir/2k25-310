using Xunit;
using Factories;
using Modules;

namespace SmartCity.Tests
{
    public class FactoryTests
    {
        [Theory]
        [InlineData("Lighting", typeof(LightingSubsystem))]
        [InlineData("Transport", typeof(TransportSubsystem))]
        [InlineData("Security", typeof(SecuritySubsystem))]
        [InlineData("Energy", typeof(EnergySubsystem))]
        public void Factory_ShouldCreateCorrectSubsystem(string type, System.Type expected)
        {
            var factory = new SubsystemFactory();

            var subsystem = factory.CreateSubsystem(type);

            Assert.IsType(expected, subsystem);
        }
    }
}
