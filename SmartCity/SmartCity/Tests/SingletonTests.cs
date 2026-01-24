using Xunit;
using Core;

namespace SmartCity.Tests
{
    public class SingletonTests
    {
        [Fact]
        public void CentralController_ShouldBeSingleton()
        {
            var instance1 = CentralController.Instance;
            var instance2 = CentralController.Instance;

            Assert.Same(instance1, instance2);
        }
    }
}
