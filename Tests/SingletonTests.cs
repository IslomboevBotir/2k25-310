using SmartCity.Core;
using Xunit;

namespace SmartCity.Tests;

public sealed class SingletonTests
{
    [Fact]
    public void Controller_Instance_IsSingleton()
    {
        var first = SmartCityController.Instance;
        var second = SmartCityController.Instance;

        Assert.Same(first, second);
    }
}