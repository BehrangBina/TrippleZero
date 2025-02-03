using Microsoft.Playwright;
using TrippleZero.Common;
using TrippleZero.Online.Pages;
using TrippleZero.Online.Utils;

namespace TrippleZero.Online.StepDefinitions
{
    /// <summary>
    /// Step definitions for logout scenarios.
    /// </summary>
    [Binding]
    public class LogoutStepDefinitions : TestBase
    {
        private readonly LogoutPage _logoutPage;
        private readonly ILogger _logger;
        private static readonly string BrowserType = EnvironmentManager.GetOrThrow("BrowserType");
        private readonly ScenarioContext _scenarioContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogoutStepDefinitions"/> class.
        /// </summary>
        /// <param name="scenarioContext">The scenario context.</param>
        /// <param name="output">The test output helper.</param>
        public LogoutStepDefinitions(ScenarioContext scenarioContext, ITestOutputHelper output) : base(BrowserType)
        {
            _logger = output.ToLogger<LogoutStepDefinitions>();
            _logger.LogInformation($"Browser Type: {BrowserType}");
            _scenarioContext = scenarioContext;

            _logoutPage = new LogoutPage(_page ?? _scenarioContext.Get<IPage>("currentPage"), _scenarioContext, _logger);
        }

        /// <summary>
        /// Verifies that the dropdown menu exists.
        /// </summary>
        [Given("The Dropdown Exists")]
        public void GivenTheDropdownExists()
        {
            _logoutPage.CheckDropdownExists();
        }

        /// <summary>
        /// Clicks on the dropdown menu.
        /// </summary>
        [When("The user clicks on the Dropdown")]
        public async Task WhenTheUserClicksOnTheDropdown()
        {
            await _logoutPage.ClickOnDropdown();
        }

        /// <summary>
        /// Verifies that the logout button is visible.
        /// </summary>
        [Then("The user sees the Logout button")]
        public async Task ThenTheUserSeesTheLogoutButton()
        {
            await _logoutPage.CheckLogoutButton();
        }

        /// <summary>
        /// Clicks on the logout button.
        /// </summary>
        [When("The user clicks on the Logout button")]
        public async Task WhenTheUserClicksOnTheLogoutButton()
        {
            await _logoutPage.ClickOnLogoutButton();
        }

        /// <summary>
        /// Verifies that the user is logged out.
        /// </summary>
        [Then("The user is logged out")]
        public async Task ThenTheUserIsLoggedOut()
        {
            await _logoutPage.CheckThatUserIsLogout();
        }
    }
}

