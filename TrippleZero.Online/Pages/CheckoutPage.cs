using Microsoft.Playwright;

namespace TrippleZero.Online.Pages
{
    /// <summary>
    /// Represents the checkout page of the application.
    /// </summary>
    internal class CheckoutPage
    {
        private readonly IPage _page;
        private ScenarioContext _scenarioContext;
        private ILogger _logger;
        private const string FirstName = "[data-test='firstName']";
        private const string LastName = "[data-test='lastName']";
        private const string PostCode = "[data-test='postalCode']";
        private const string Continue = "[data-test='continue']";
        //checkout page part 2
        private const string ProductName = "[data-test='inventory-item-name']";
        private const string ProductContainerPrice = "[data-test='inventory-item-price']";
        private const string InformationLabel = "[data-test='payment-info-label']";
        private const string InformationLabelValue = "[data-test='payment-info-value']";
        private const string ShoppingInfoLable = "[data-test='shipping-info-label']";
        private const string ShoppingInfoLableValue = "[data-test='shipping-info-value']";
        private const string FinishButton = "[data-test='finish']";
        private const string TotalInfoLabel = "[data-test='total-info-label']";
        private const string TotalInfoValue = "[data-test='subtotal-label']";
        private const string TaxLabel = "[data-test='tax-label']";

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckoutPage"/> class.
        /// </summary>
        /// <param name="page">The Playwright page instance.</param>
        /// <param name="ScenarioContext">The scenario context.</param>
        /// <param name="logger">The logger instance.</param>
        public CheckoutPage(IPage page, ScenarioContext ScenarioContext, ILogger logger)
        {
            _page = page;
            _scenarioContext = ScenarioContext;
            _logger = logger;
        }

        /// <summary>
        /// Fills the checkout form with the provided details.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="postalCode">The postal code.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        internal async Task FillCheckoutForm(string firstName, string lastName, string postalCode)
        {
            await FillFirstName(firstName);
            await FillLastName(lastName);
            await FillPostalCode(postalCode);
        }

        /// <summary>
        /// Clicks the continue button on the checkout page.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        internal async Task ClickContinueOnCheckout() => await ClickContinue();

        /// <summary>
        /// Fills the first name field.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        private async Task FillFirstName(string firstName)
        {
            _logger.LogInformation($"Filling First Name: {firstName}");
            await _page.Locator(FirstName).FillAsync(firstName);
        }

        /// <summary>
        /// Fills the last name field.
        /// </summary>
        /// <param name="lastName">The last name.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        private async Task FillLastName(string lastName)
        {
            _logger.LogInformation($"Filling Last Name: {lastName}");
            await _page.Locator(LastName).FillAsync(lastName);
        }

        /// <summary>
        /// Fills the postal code field.
        /// </summary>
        /// <param name="postalCode">The postal code.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        private async Task FillPostalCode(string postalCode)
        {
            _logger.LogInformation($"Filling PostCode: {postalCode}");
            await _page.Locator(PostCode).FillAsync(postalCode);
        }

        /// <summary>
        /// Clicks the continue button.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        private async Task ClickContinue()
        {
            _logger.LogInformation("Clicking Continue");
            await _page.Locator(Continue).ClickAsync();
        }

        /// <summary>
        /// Clicks the finish button on the checkout page.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        internal async Task ClickFinish()
        {
            _logger.LogInformation("Clicking finish button");
            await _page.Locator(FinishButton).ClickAsync();
        }

        /// <summary>
        /// Validates the elements on the second page of the checkout.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        internal async Task ValidateCheckoutSecondPage()
        {
            await ValidateProductName();
            await ValidateProductPrice();
            await ValidateIformationElements();
            await ValidateShippingInformationElements();
            await VaidateTotalElements();
        }

        /// <summary>
        /// Validates the total elements on the checkout page.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        private async Task VaidateTotalElements()
        {
            _logger.LogInformation("Validating Total Elements");
            var price = _scenarioContext.Get<string>("productPrice");
            var totalInfoLabel = await _page.Locator(TotalInfoLabel).TextContentAsync();
            totalInfoLabel?.Trim()
                .Should().NotBeNullOrWhiteSpace("Total Info Label Should Be Visible")
                .And.Be("Price Total");
            _logger.LogInformation("Validating Total Value");
            var totalInfoValue = await _page.Locator(TotalInfoValue).TextContentAsync();
            totalInfoValue?.Trim()
                .Should().NotBeNullOrWhiteSpace("Toal Info should have value")
                .And.Contain(price, "Price should match");
            _logger.LogInformation("Validate  Total and Tax");
            var taxLabel = await _page.Locator(TaxLabel).TextContentAsync();
            taxLabel.Should().NotBeNullOrEmpty("Tax value shoudl be visible");
            decimal taxValue;
            taxLabel = taxLabel.Replace("Tax: $", "").Trim();
            decimal.TryParse(taxLabel, out taxValue);
            taxValue.Should().BeGreaterThan(0, "Tax value should be greater than 0");
        }

        /// <summary>
        /// Validates the shipping information elements on the checkout page.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        private async Task ValidateShippingInformationElements()
        {
            var shippingLabel = await _page.Locator(ShoppingInfoLable).TextContentAsync();
            shippingLabel.Should().NotBeNullOrEmpty("Shipping Information label on page is empty");
            var shippingLabelValue = await _page.Locator(ShoppingInfoLableValue).TextContentAsync();
            shippingLabelValue.Should().NotBeNullOrEmpty("Shipping Information value on page is empty");
            shippingLabel.Trim().Should().Be("Shipping Information:", $"Shipping Information label on page: {shippingLabel} does not");
            shippingLabelValue.Trim().Should().Be("Free Pony Express Delivery!", $"Shipping Information value on page: {shippingLabelValue} does not match FREE PONY EXPRESS DELIVERY!");
        }

        /// <summary>
        /// Validates the payment information elements on the checkout page.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        private async Task ValidateIformationElements()
        {
            var infoLabel = await _page.Locator(InformationLabel).TextContentAsync();
            infoLabel.Should().NotBeNullOrEmpty("Payment Information label on page is empty");

            var infoLabelValue = await _page.Locator(InformationLabelValue).TextContentAsync();
            infoLabelValue.Should().NotBeNullOrEmpty("Payment Information value on page is empty");

            infoLabel.Trim().Should().Be("Payment Information:", $"Payment Information label on page: {infoLabel} does not");
            infoLabelValue.Trim()
                .Length.Should().BeGreaterThan("SauceCard #".Length, $"Payment Information value on page: {infoLabelValue} does not match SauceCard #1-9");
        }

        /// <summary>
        /// Validates the product name on the checkout page.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        private async Task ValidateProductName()
        {
            var productName = _scenarioContext.Get<string>("productName");
            var productNameOnPage = await _page.Locator(ProductName).TextContentAsync();
            productNameOnPage.Should().Be(productName, $"Product name on page: {productNameOnPage} does not match {productName}");
        }

        /// <summary>
        /// Validates the product price on the checkout page.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        private async Task ValidateProductPrice()
        {
            var productPrice = _scenarioContext.Get<string>("productPrice");
            var productPriceOnPage = await _page.Locator(ProductContainerPrice).TextContentAsync();
            productPriceOnPage.Should().Be(productPrice, $"Product price on page: {productPriceOnPage} does not match {productPrice}");
        }
    }
}