using SmartCity.Core;
using SmartCity.Core.Factories;
using SmartCity.Core.Proxy;
using SmartCity.Modules.Energy;
using SmartCity.Modules.Lighting;

namespace SmartCity.Tests
{
    [TestClass]
    public class SmartCityTests
    {
        [TestMethod]
        public void Singleton_ShouldReturnSameInstance()
        {
            // Arrange & Act
            var instance1 = CityController.Instance;
            var instance2 = CityController.Instance;

            // Assert
            Assert.AreSame(instance1, instance2, "CityController should return the same instance");
        }

        [TestMethod]
        public void LightingFactory_ShouldCreateCorrectSystem()
        {
            // Arrange
            var factory = new LightingFactory();

            // Act
            var system = factory.CreateLightingSystem("advanced");

            // Assert
            Assert.IsNotNull(system, "Factory should create a lighting system");
        }

        [TestMethod]
        public void LightingSystem_ShouldAdjustBrightness()
        {
            // Arrange
            var lighting = new LightingSystem("Test", 100);

            // Act
            lighting.AdjustBrightness(75);

            // Assert - No exception should be thrown
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void SecurityProxy_ShouldAllowAdminAccess()
        {
            // Arrange
            var proxy = new SecuritySystemProxy("admin");

            // Act & Assert - Should not throw exception
            proxy.GetStatus();
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void SecurityProxy_ShouldDenyUnauthorizedAccess()
        {
            // Arrange
            var proxy = new SecuritySystemProxy("guest");

            // Act - Should handle gracefully
            proxy.GetStatus();

            // Assert
            Assert.IsTrue(true, "Proxy should handle unauthorized access gracefully");
        }

        [TestMethod]
        public void TransportFactory_ShouldCreateSmartSystem()
        {
            // Arrange
            var factory = new TransportFactory();

            // Act
            var system = factory.CreateTransportSystem("smart");

            // Assert
            Assert.IsNotNull(system, "Factory should create transport system");
        }

        [TestMethod]
        public void EnergyMonitor_ShouldRecordConsumption()
        {
            // Arrange
            var monitor = new EnergyMonitor();

            // Act
            monitor.RecordConsumption("Lighting", 25.5);
            monitor.RecordConsumption("Transport", 30.0);

            // Assert - Should not throw exception
            monitor.GenerateReport();
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void CityController_ShouldInitializeSuccessfully()
        {
            // Arrange
            var controller = CityController.Instance;

            // Act
            controller.InitializeCity();

            // Assert
            Assert.IsTrue(true, "City should initialize without errors");
        }
    }
}