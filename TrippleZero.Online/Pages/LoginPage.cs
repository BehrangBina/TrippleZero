using Microsoft.Playwright;

namespace TrippleZero.Online.Pages
{
    /// <summary>
    /// Represents the Login page of the application.
    /// </summary>
    public class LoginPage
    {
        private readonly IPage _page;
        private ScenarioContext _scenarioContext;
        private ILogger _logger;

        private string UserName = "#user-name";
        private string LoginButton = "#login-button";
        private string ErrorMessage = ".error-message-container";
        private string Password = "#password";

        public LoginPage(IPage page, ScenarioContext scenarioContext, ILogger logger)
        {
            _page = page;
            _scenarioContext = scenarioContext;
            _logger = logger;
        }

        /// <summary>
        /// Enters the username into the username field.
        /// </summary>
        /// <param name="username">The username to enter.</param>
        public async Task EnterUsername(string username)
        {
            _logger.LogInformation("Entering username {0}", username);
            await _page.FillAsync(UserName, username);
        }

        /// <summary>
        /// Enters the password into the password field.
        /// </summary>
        /// <param name="password">The password to enter.</param>
        public async Task EnterPassword(string password)
        {
            _logger.LogInformation("Entering password {0}", password);
            await _page.FillAsync(Password, password);
        }

        /// <summary>
        /// Clicks the login button.
        /// </summary>
        public async Task ClickLogin()
        {
            _logger.LogInformation("Clicking on login button");
            await _page.ClickAsync(LoginButton);
        }

        /// <summary>
        /// Gets the error message displayed on the login page.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the error message text.</returns>
        public async Task<string> GetErrorMessage()
        {
            _logger.LogInformation("Getting error message");
            return await _page.InnerTextAsync(ErrorMessage);
        }
    }
}
