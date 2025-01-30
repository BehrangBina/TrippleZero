
using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;

namespace TrippleZero.Utils
{
  public class TestBase:IAsyncLifetime
    {
        protected IPlaywright _playwright;
        protected IBrowser _browser;
        protected IPage _page;
        private readonly string _browserType;

        public TestBase(string browserType)
        {
            var configuration = SetupAppsettings();
            _browserType = configuration["BrowserType"] ?? "chromium";           
        }

        public async Task InitializeAsync()
        {
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright[_browserType].LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
            _page = await _browser.NewPageAsync();
            await _page.GotoAsync("https://www.saucedemo.com");
        }

        public async Task DisposeAsync()
        {
            await _browser.CloseAsync();
            _playwright.Dispose();
        }

        private IConfiguration SetupAppsettings()
        {
           return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(Environment.GetCommandLineArgs())
                .Build(); 
        }
    }
}
