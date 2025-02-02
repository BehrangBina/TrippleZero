using Microsoft.Playwright;
using TrippleZero.Common;
using TrippleZero.Pages;
using TrippleZero.Utils;

namespace TrippleZero.StepDefinitions
{
    [Binding]
    public class LogoutStepDefinitions : TestBase
    {
        private readonly LogoutPage _logoutPage;
        private ILogger  _logger;
        private static string _browserType = EnvironmentManager.GetOrThrow("BrowserType");
        private ScenarioContext _scenarioContext;
        public LogoutStepDefinitions(ScenarioContext scenarioContext, ITestOutputHelper output) : base(_browserType) // You can change "chromium" to any browser type you want to use
        {
            _logger = output.ToLogger<LogoutStepDefinitions>();
            _logger.LogInformation($"Browser Type: {_browserType}");
            _scenarioContext = scenarioContext;

            _logoutPage = new LogoutPage(_page ?? _scenarioContext.Get<IPage>("currentPage"), _scenarioContext, _logger);
        }
        [Given("The Dropdown Exists")]
        public void GivenTheDropdownExists()
        {
            _logoutPage.CheckDropdownExists();
        }

        [When("The user clicks on the Dropdown")]
        public async Task WhenTheUserClicksOnTheDropdown()
        {
            await _logoutPage.ClickOnDropdown();
        }

        [Then("The user sees the Logout button")]
        public async Task ThenTheUserSeesTheLogoutButton()
        {
           await _logoutPage.CheckLogoutButton();
        }

        [When("The user clicks on the Logout button")]
        public async Task WhenTheUserClicksOnTheLogoutButton()
        {
            await _logoutPage.ClickOnLogoutButton();
        }

        [Then("The user is logged out")]
        public async Task ThenTheUserIsLoggedOut()
        {
            await _logoutPage.CheckThatUserIsLogout();
        }
    }
}
