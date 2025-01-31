using Microsoft.Extensions.Logging;
using TrippleZero.Common;
using Xunit.Abstractions;

namespace TrippleZero
{
    public class ConfigurationManagerTests
    {
        private readonly ILogger _logger;
        public ConfigurationManagerTests(ITestOutputHelper output)
        {
            _logger = output.ToLogger<ConfigurationManagerTests>();
        }

        [Theory(DisplayName = "Can Read Config Value")]
        [Trait("Category", "Unit")]
        [InlineData("BaseUrl", "https://www.saucedemo.com/")]
        [InlineData("BrowserType", "chromium")]
        [InlineData("Standard_user", "standard_user")]
        [InlineData("locked_out_user", "locked_out_user")]
        [InlineData("problem_user", "problem_user")]
        [InlineData("performance_glitch_user", "performance_glitch_user")]
        [InlineData("error_user", "error_user")]
        [InlineData("visual_user", "visual_user")]
        [InlineData("password", "secret_sauce")]
        public void TestConfigurationValues(string key, string expectedValue)
        {
            var value = EnvironmentManager.GetOrThrow(key);
            _logger.LogInformation($"Key: {key}, Value: {value}");
            Assert.Equal(expectedValue, value);
        }
    }
}
