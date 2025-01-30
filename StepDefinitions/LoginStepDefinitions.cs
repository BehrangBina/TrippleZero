using Microsoft.Extensions.Logging;
using TrippleZero.Pages;
using TrippleZero.Utils;
using Xunit.Abstractions;

namespace TrippleZero.StepDefinitions
{
    [Binding]
    public class LoginStepDefinitions: TestBase
    {
        private readonly LoginPage? _loginPage;
        private ILogger _logger;

        public LoginStepDefinitions(string browserType,ITestOutputHelper output) : base(browserType)
        {
            _logger = output.ToLogger<LoginStepDefinitions>();
            _logger.LogInformation("Using Browser {0}", browserType);
        }

        [Given("I navigate to the login page")]
        public void GivenINavigateToTheLoginPage()
        {
            throw new PendingStepException();
        }

        [When("I enter valid username and password")]
        public void WhenIEnterValidUsernameAndPassword()
        {
            throw new PendingStepException();
        }

        [When("I click the login button")]
        public void WhenIClickTheLoginButton()
        {
            throw new PendingStepException();
        }

        [Then("I should be redirected to the inventory page")]
        public void ThenIShouldBeRedirectedToTheInventoryPage()
        {
            throw new PendingStepException();
        }

        [When("I enter invalid username and password")]
        public void WhenIEnterInvalidUsernameAndPassword()
        {
            throw new PendingStepException();
        }

        [Then("I should see an error message")]
        public void ThenIShouldSeeAnErrorMessage()
        {
            throw new PendingStepException();
        }
    }
}
