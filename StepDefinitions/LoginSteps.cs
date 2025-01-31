using Microsoft.Extensions.Logging;
using TrippleZero.Common;
using TrippleZero.Pages;
using TrippleZero.Utils;
using Xunit.Abstractions;

namespace TrippleZero.StepDefinitions
{
    [Binding]
    public class LoginSteps : TestBase
    {
        private readonly LoginPage _loginPage;
        private ILogger<LoginSteps> _logger;
        private static string _browserType = EnvironmentManager.GetOrThrow("BrowserType");

        public LoginSteps(ITestOutputHelper output) : base(_browserType) // You can change "chromium" to any browser type you want to use
        {
            _logger = output.ToLogger<LoginSteps>();
            InitializeAsync().GetAwaiter().GetResult();
            _loginPage = new LoginPage(_page);
        }

        [Given("I navigate to the login page")]
        public async Task GivenINavigateToTheLoginPage()
        {
            await _page.GotoAsync("https://www.saucedemo.com");
        }

        [When("I enter valid username and password")]
        public async Task WhenIEnterValidUsernameAndPassword()
        {
            await _loginPage.EnterUsername("standard_user");
            await _loginPage.EnterPassword("secret_sauce");
        }

        [When("I enter invalid username and password")]
        public async Task WhenIEnterInvalidUsernameAndPassword()
        {
            await _loginPage.EnterUsername("invalid_user");
            await _loginPage.EnterPassword("invalid_password");
        }

        [When("I click the login button")]
        public async Task WhenIClickTheLoginButton()
        {
            await _loginPage.ClickLogin();
        }

        [Then("I should be redirected to the inventory page")]
        public void ThenIShouldBeRedirectedToTheInventoryPage()
        {
            Assert.Equal("https://www.saucedemo.com/inventory.html", _page.Url);
        }

        [Then("I should see an error message")]
        public async Task ThenIShouldSeeAnErrorMessage()
        {
            var errorMessage = await _loginPage.GetErrorMessage();
            Assert.Contains("Username and password do not match any user in this service", errorMessage);
        }
    }
}
