using TrippleZero.Common;
using TrippleZero.Pages;
using TrippleZero.Utils;

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
            _logger.LogInformation($"Browser Type: {_browserType}");
            InitializeAsync().GetAwaiter().GetResult();
            _loginPage = new LoginPage(_page);
        }

        [Given("I navigate to the login page")]
        public async Task GivenINavigateToTheLoginPage()
        {
            var url = Endpoints.BaseUrl;
            _logger.LogInformation($"Navigating to {url}");
            await _page.GotoAsync(url);
        }

        [When("I enter valid username and password")]
        public async Task WhenIEnterValidUsernameAndPassword()
        {
            var username = EnvironmentManager.GetOrThrow("Standard_user");
            var password = EnvironmentManager.GetOrThrow("Password");
            _logger.LogInformation($"Username: {username}, Password: {password}");
            await _loginPage.EnterUsername(username);
            await _loginPage.EnterPassword(password);
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
            var expectedUrl = Endpoints.InventoryPage;
            expectedUrl.Should().Be(_page.Url,$"Url should be {expectedUrl}");           
        }

        [Then("I should see an error message")]
        public async Task ThenIShouldSeeAnErrorMessage()
        {
            var errorMessage = await _loginPage.GetErrorMessage();
            errorMessage
                .Should()
                .NotBeNullOrEmpty("Error message should not be empty")
                .And
                .Be("Username and password do not match any user in this service");
        }
    }
}
