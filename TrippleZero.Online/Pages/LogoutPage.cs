using Microsoft.Playwright;
using TrippleZero.Online.Utils;

namespace TrippleZero.Online.Pages
{
    /// <summary>
    /// Represents the Logout page of the application.
    /// </summary>
    internal class LogoutPage
    {
        private readonly IPage _page;
        private ScenarioContext _scenarioContext;
        private ILogger _logger;
        private string DropDownMenu = "#react-burger-menu-btn";
        private string LogoutSidebarLink = "[data-test='logout-sidebar-link']";

        public LogoutPage(IPage page, ScenarioContext ScenarioContext, ILogger logger)
        {
            _page = page;
            _scenarioContext = ScenarioContext;
            _logger = logger;
        }

        internal void CheckDropdownExists()
        {
            _logger.LogInformation("Checking if dropdown exists");
            var menu = _page.QuerySelectorAsync(DropDownMenu);
            menu.Should().NotBeNull("Dropdown should be visible");
        }

        internal async Task CheckLogoutButton()
        {
            _logger.LogInformation("Checking if logout button exists");
            var logoutButton = await _page.QuerySelectorAsync(LogoutSidebarLink);
            logoutButton.Should().NotBeNull("Logout Link should exist");
        }

        internal async Task CheckThatUserIsLogout()
        {
            _logger.LogInformation("Checking if user is logged out");
            var url = _page.Url;
            url.Should().Contain(Endpoints.BaseUrl, "User should be in home page");
            _logger.LogInformation("Checking if dropdown exists");
            var menu = await _page.QuerySelectorAsync(DropDownMenu);
            menu.Should().BeNull("Dropdown should not be visible");
        }

        internal async Task ClickOnDropdown()
        {
            _logger.LogInformation("Clicking on dropdown");
            var menu = await _page.QuerySelectorAsync(DropDownMenu);
            menu.Should().NotBeNull("Dropdown should be visible");
            await menu.ClickAsync();
        }

        internal async Task ClickOnLogoutButton()
        {
            _logger.LogInformation("Clicking on logout button");
            var logoutButton = await _page.QuerySelectorAsync(LogoutSidebarLink);
            logoutButton.Should().NotBeNull("Logout button should be visible");
            await logoutButton.ClickAsync();
        }
    }
}
