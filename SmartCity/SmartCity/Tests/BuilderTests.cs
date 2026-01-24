using Xunit;
using Builders;

namespace SmartCity.Tests
{
    public class BuilderTests
    {
        [Fact]
        public void Builder_ShouldCreateReport()
        {
            var builder = new ReportBuilder();

            var report = builder
                .StartReport("Test Report")
                .AddSection("Section1", "Hello")
                .Finish();

            Assert.Contains("Test Report", report);
            Assert.Contains("Section1", report);
            Assert.Contains("Hello", report);
        }
    }
}
