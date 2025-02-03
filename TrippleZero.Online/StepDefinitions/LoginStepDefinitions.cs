using Microsoft.Playwright;
using TrippleZero.Common;
using TrippleZero.Online.Pages;
using TrippleZero.Online.Utils;
namespace TrippleZero.Online.StepDefinitions
{
    /// <summary>
    /// Step definitions for login feature.
    /// </summary>
    [Binding]
    public class LoginStepDefinitions : TestBase
    {
        private readonly LoginPage _loginPage;
        private ILogger<LoginStepDefinitions> _logger;
        private static string _browserType = EnvironmentManager.GetOrThrow("BrowserType");
        private ScenarioContext _scenarioContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginStepDefinitions"/> class.
        /// </summary>
        /// <param name="scenarioContext">The scenario context.</param>
        /// <param name="output">The test output helper.</param>
        public LoginStepDefinitions(ScenarioContext scenarioContext, ITestOutputHelper output) : base(_browserType)
        {
            _logger = output.ToLogger<LoginStepDefinitions>();
            _logger.LogInformation($"Browser Type: {_browserType}");
            InitializeAsync().GetAwaiter().GetResult();
            _scenarioContext = scenarioContext;
            _loginPage = new LoginPage(_page ?? _scenarioContext.Get<IPage>("currentPage"), _scenarioContext, _logger);     
       
        }

        /// <summary>
        /// Navigates to the login page.
        /// </summary>
        [Given("I navigate to the login page")]
        public async Task GivenINavigateToTheLoginPage()
        {
            var url = Endpoints.BaseUrl;
            _logger.LogInformation($"Navigating to {url}");
            await _page.GotoAsync(url);
        }

        /// <summary>
        /// Enters valid username and password.
        /// </summary>
        [When("I enter valid username and password")]
        public async Task WhenIEnterValidUsernameAndPassword()
        {
            var username = EnvironmentManager.GetOrThrow("Standard_user");
            var password = EnvironmentManager.GetOrThrow("Password");
            _logger.LogInformation($"Username: {username}, Password: {password}");
            await _loginPage.EnterUsername(username);
            await _loginPage.EnterPassword(password);
        }

        /// <summary>
        /// Enters invalid username and password.
        /// </summary>
        [When("I enter invalid username and password")]
        public async Task WhenIEnterInvalidUsernameAndPassword()
        {
            await _loginPage.EnterUsername("invalid_user");
            await _loginPage.EnterPassword("invalid_password");
        }

        /// <summary>
        /// Clicks the login button.
        /// </summary>
        [When("I click the login button")]
        public async Task WhenIClickTheLoginButton()
        {
            await _loginPage.ClickLogin();
            _scenarioContext.Add("currentUrl", _page.Url);
            _scenarioContext.Add("currentPage", _page);
        }

        /// <summary>
        /// Verifies redirection to the inventory page.
        /// </summary>
        [Then("I should be redirected to the inventory page")]
        public void ThenIShouldBeRedirectedToTheInventoryPage()
        {
            var expectedUrl = Endpoints.InventoryPage;
            expectedUrl.Should().Be(_page.Url, $"Url should be {expectedUrl}");
        }

        /// <summary>
        /// Verifies the presence of an error message.
        /// </summary>
        [Then("I should see an error message")]
        public async Task ThenIShouldSeeAnErrorMessage()
        {
            var errorMessage = await _loginPage.GetErrorMessage();
            errorMessage
                .Should()
                .NotBeNullOrEmpty("Error message should not be empty")
                .And
                .Be("Epic sadface: Username and password do not match any user in this service", "Error Message Did Not Match");
        }
    }
}
