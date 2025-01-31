
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

        public TestBase(string browserType) => _browserType = browserType;

        public async Task InitializeAsync()
        {
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright[_browserType].LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
            _page = await _browser.NewPageAsync();
            await _page.SetViewportSizeAsync(1920, 1080);
            await _page.GotoAsync(Endpoints.BaseUrl);
        }

        public async Task DisposeAsync()
        {
            await _browser.CloseAsync();
            _playwright.Dispose();
        }

    }
}
