using Microsoft.Playwright;

namespace TrippleZero.Online.Pages
{
    /// <summary>
    /// Represents the Checkout Complete page and provides methods to validate its elements.
    /// </summary>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckoutCompletePage"/> class.
        /// </summary>
        /// <param name="page">The Playwright page instance.</param>
        /// <param name="ScenarioContext">The scenario context.</param>
        /// <param name="logger">The logger instance.</param>
        public CheckoutCompletePage(IPage page, ScenarioContext ScenarioContext, ILogger logger)
        {
            _page = page;
            _scenarioContext = ScenarioContext;
            _logger = logger;
        }

        /// <summary>
        /// Checks the page title and validates it.
        /// </summary>
        private async Task CheckPageTitle()
        {
            var pageTitle = await _page.QuerySelectorAsync(PageTitle);
            var pageTitleText = await pageTitle!.TextContentAsync();
            _logger.LogInformation($"Page Title: {pageTitleText!.Trim()}");
            pageTitleText!.Trim().Should().Be(PageTitleText, "Page Title should match");
        }

        /// <summary>
        /// Checks the complete header and validates it.
        /// </summary>
        internal async Task CheckCompleteHeader()
        {
            var completeHeader = await _page.QuerySelectorAsync(CompleteHeader);
            var completeHeaderText = await completeHeader!.TextContentAsync();
            _logger.LogInformation($"Complete Header: {completeHeaderText!.Trim()}");
            completeHeaderText!.Trim().Should().Be(CompleteHeaderText, "Complete Header should match");
        }

        /// <summary>
        /// Checks the complete text and validates it.
        /// </summary>
        internal async Task CheckCompleteText()
        {
            var completeText = await _page.QuerySelectorAsync(CompleteText);
            var completeTextText = await completeText!.TextContentAsync();
            _logger.LogInformation($"Complete Text: {completeTextText!.Trim()}");
            completeTextText!.Trim().Should().Be(CompleteTextText, "Complete Text should match");
        }

        /// <summary>
        /// Checks the back to home button and validates its visibility.
        /// </summary>
        internal async Task CheckBackToHomeButton()
        {
            var backToHomeButton = await _page.QuerySelectorAsync(BackToHomeButton);
            backToHomeButton.Should().NotBeNull("Back to Home button should be visible");
        }

        /// <summary>
        /// Validates the thank you message by checking the page title, complete header, complete text, and back to home button.
        /// </summary>
        internal async Task ValidateThankYouMessage()
        {
            await CheckPageTitle();
            await CheckCompleteHeader();
            await CheckCompleteText();
            await CheckBackToHomeButton();
        }
    }
}
