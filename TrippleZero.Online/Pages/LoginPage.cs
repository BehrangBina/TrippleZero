using Microsoft.Playwright;

namespace TrippleZero.Online.Pages
{
    public class LoginPage
    {
        private readonly IPage _page;

        public LoginPage(IPage page) => _page = page;

        public async Task EnterUsername(string username)
        {
            await _page.FillAsync("#user-name", username);
        }

        public async Task EnterPassword(string password)
        {
            await _page.FillAsync("#password", password);
        }

        public async Task ClickLogin()
        {
            await _page.ClickAsync("#login-button");
        }

        public async Task<string> GetErrorMessage()
        {
            return await _page.InnerTextAsync(".error-message-container");
        }
    }
}
