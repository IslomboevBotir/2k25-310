using Xunit;
using Modules;
using Decorators;

namespace SmartCity.Tests
{
    public class DecoratorTests
    {
        [Fact]
        public void Decorator_ShouldNotChangeOriginalBehavior()
        {
            var security = new SecuritySubsystem();
            var decorated = new LoggingDecorator(security);

            decorated.Start();

            Assert.Equal("Security Armed", decorated.Status);
        }
    }
}
