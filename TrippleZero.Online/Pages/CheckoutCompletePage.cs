using Microsoft.Playwright;

namespace TrippleZero.Online.Pages
{
    internal class CheckoutCompletePage
    {
        private readonly IPage _page;
        private ScenarioContext _scenarioContext;
        private ILogger _logger;
        private string PageTitle = "[data-test='title']";
        private string CompleteHeader = "[data-test='complete-header']";
        private string CompleteText = "[data-test='complete-text']";
        private string BackToHomeButton = "[data-test='back-to-products']";

        private const string PageTitleText = "Checkout: Complete!";
        private const string CompleteHeaderText = "Thank you for your order!";
        private const string CompleteTextText = "Your order has been dispatched, and will arrive just as fast as the pony can get there!";


        public CheckoutCompletePage(IPage page, ScenarioContext ScenarioContext, ILogger logger)
        {
            _page = page;
            _scenarioContext = ScenarioContext;
            _logger = logger;
        }
        private async Task CheckPageTitle()
        {
            var pageTitle = await _page.QuerySelectorAsync(PageTitle);
            var pageTitleText = await pageTitle!.TextContentAsync();
            _logger.LogInformation($"Page Title: {pageTitleText!.Trim()}");
            pageTitleText!.Trim().Should().Be(PageTitleText, "Page Title should match");
        }
        internal async Task CheckCompleteHeader()
        {
            var completeHeader = await _page.QuerySelectorAsync(CompleteHeader);
            var completeHeaderText = await completeHeader!.TextContentAsync();
            _logger.LogInformation($"Complete Header: {completeHeaderText!.Trim()}");
            completeHeaderText!.Trim().Should().Be(CompleteHeaderText, "Complete Header should match");
        }
        internal async Task CheckCompleteText()
        {
            var completeText = await _page.QuerySelectorAsync(CompleteText);
            var completeTextText = await completeText!.TextContentAsync();
            _logger.LogInformation($"Complete Text: {completeTextText!.Trim()}");
            completeTextText!.Trim().Should().Be(CompleteTextText, "Complete Text should match");
        }
        internal async Task CheckBackToHomeButton()
        {
            var backToHomeButton = await _page.QuerySelectorAsync(BackToHomeButton);
            backToHomeButton.Should().NotBeNull("Back to Home button should be visible");
        }

        internal async Task ValidateThankYouMessage()
        {
            await CheckPageTitle();
            await CheckCompleteHeader();
            await CheckCompleteText();
            await CheckBackToHomeButton();
        }
    }
}
