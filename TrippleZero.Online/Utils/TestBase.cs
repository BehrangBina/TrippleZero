using Microsoft.Playwright;

namespace TrippleZero.Online.Utils
{
    /// <summary>
    /// Base class for Playwright tests, providing setup and teardown functionality.
    /// </summary>
    public class TestBase : IAsyncLifetime
    {
        protected IPlaywright? _playwright;
        protected IBrowser? _browser;
        protected IPage? _page;
        private readonly string _browserType;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestBase"/> class.
        /// </summary>
        /// <param name="browserType">The type of browser to use for the tests.</param>
        public TestBase(string browserType) => _browserType = browserType;

        /// <summary>
        /// Initializes the Playwright, browser, and page instances.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task InitializeAsync()
        {
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright[_browserType].LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
            _page = await _browser.NewPageAsync();
            await _page.SetViewportSizeAsync(1920, 1080);
            // Set test id attribute to all elements
            await _page.GotoAsync(Endpoints.BaseUrl);
        }

        /// <summary>
        /// Disposes the Playwright and browser instances.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task DisposeAsync()
        {
            if (_browser != null) await _browser.CloseAsync();
            _playwright?.Dispose();
        }
    }
}
