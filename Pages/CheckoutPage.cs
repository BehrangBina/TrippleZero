using Microsoft.Playwright;

namespace TrippleZero.Pages
{
    internal class CheckoutPage
    {
        private readonly IPage _page;
        private ScenarioContext _scenarioContext;
        private ILogger _logger;
        private const string FirstName = "[data-test='firstName']";
        private const string LastName = "[data-test='lastName']";
        private const string PostCode = "[data-test='postalCode']";
        private const string Continue = "[data-test='continue']";

        public CheckoutPage(IPage page, ScenarioContext ScenarioContext, ILogger logger)
        {
            _page = page;
            _scenarioContext = ScenarioContext;
            _logger = logger;
        }

        internal async Task FillCheckoutForm(string firstName, string lastName, string postalCode)
        {
            await FillFirstName(firstName);
            await FillLastName(lastName);
            await FillPostalCode(postalCode);            
        }

        internal async Task ClickContinueOnCheckout() => await ClickContinue();     
    
        private async Task FillFirstName(string firstName)
        {
            _logger.LogInformation($"Filling First Name: {firstName}");
            await _page.Locator(FirstName).FillAsync(firstName);
        }

        private async Task FillLastName(string lastName)
        {
            _logger.LogInformation($"Filling Last Name: {lastName}");
            await _page.Locator(LastName).FillAsync(lastName);
        }

        private async Task FillPostalCode(string postalCode)
        {
            _logger.LogInformation($"Filling PostCode: {postalCode}");
            await _page.Locator(PostCode).FillAsync(postalCode);
        }

        private async Task ClickContinue()
        {
            _logger.LogInformation("Clicking Continue");
            await _page.Locator(Continue).ClickAsync();
        }

    }
}
