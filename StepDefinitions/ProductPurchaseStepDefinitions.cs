using Microsoft.Playwright;
using TrippleZero.Common;
using TrippleZero.Pages;
using TrippleZero.Utils;

namespace TrippleZero.StepDefinitions
{
    [Binding]
    public class ProductPurchaseStepDefinitions : TestBase
    {
        private readonly InventoryPage _inventoryPage;
        private readonly CartPage _cartPage;
        private readonly CheckoutPage _checkoutPage;
        private readonly CheckoutCompletePage _checkoutCompetePage;

        private ILogger<LoginStepDefinitions> _logger;
        private static string _browserType = EnvironmentManager.GetOrThrow("BrowserType");
        private ScenarioContext _scenarioContext;
        public ProductPurchaseStepDefinitions(ScenarioContext scenarioContext, ITestOutputHelper output) : base(_browserType) 
        {
            _logger = output.ToLogger<LoginStepDefinitions>();
            _logger.LogInformation($"Browser Type: {_browserType}");
            _scenarioContext = scenarioContext;

            _inventoryPage = new InventoryPage(_page ?? _scenarioContext.Get<IPage>("currentPage"), _scenarioContext, _logger);
            _cartPage = new CartPage(_page ?? _scenarioContext.Get<IPage>("currentPage"), _scenarioContext, _logger);
            _checkoutPage = new CheckoutPage(_page ?? _scenarioContext.Get<IPage>("currentPage"), _scenarioContext, _logger);
            _checkoutCompetePage = new CheckoutCompletePage(_page ?? _scenarioContext.Get<IPage>("currentPage"), _scenarioContext, _logger);
        }


        [Given("I am in the inventory page")]
        public void GivenIAmInTheInventoryPage()
        {
            _logger.LogInformation($"Checking if user is in {Endpoints.InventoryPage}");
            _scenarioContext.Get<string>("currentUrl").Should().Be(Endpoints.InventoryPage, "Current Url Should Match");
        }

        [When("From products list I Add to cart {string}")]
        public async Task WhenFromProductsListIAddToCartAsync(string productName)
        {
            _logger.LogInformation($"Adding {productName} to cart");
            await _inventoryPage.AddProductToCart(productName);
        }

        [Then("I add to cart button should change to remove")]
        public async Task ThenIAddToCartButtonShouldChangeToRemoveAsync()
        {
            var productName = _scenarioContext.Get<string>("productName");
            await _inventoryPage.CheckProductAddToCardChangedAfterClicking(productName);
        }



        [Then("I should see the cart badge with {int}")]
        public async Task ThenIShouldSeeTheCartBadgeWith(int itemsInCart)
        {
            await _inventoryPage.CheckItemsInCartBadge(itemsInCart, _logger);
            _scenarioContext.Add("productQty", itemsInCart);
        }

        [When("I click the cart button")]
        public async Task WhenIClickTheCartButton()
        {
            await _inventoryPage.ClickCartButton();
        }

        [Then("I should see the product in the cart")]
        public async Task ThenIShouldSeeTheProductInTheCartAsync()
        {
            var productName = _scenarioContext.Get<string>("productName");
            _logger.LogInformation($"Checking if {productName} is in the cart");
            await _cartPage.CheckProductInCart(productName);
        }

        [Then("I should see the product price")]
        public async Task ThenIShouldSeeTheProductPrice()
        {
            _logger.LogInformation($"Checking if price is visible in the cart");
            await _cartPage.CheckProductPrice();

        }

        [Then("I should see the checkout button")]
        public async Task ThenIShouldSeeTheCheckoutButton()
        {
            _logger.LogInformation($"Checking if checkout button is visible in the cart");
            await _cartPage.CheckCheckoutButton();
        }

        [When("I click the checkout button")]
        public async Task WhenIClickTheCheckoutButton()
        {
            _logger.LogInformation("Click on Checkout button");
            await _cartPage.ClickCheckoutButton();
        }

        [Then("I am on {string} page")]
        public void ThenIAmOnPage(string pageName)
        {
            _logger.LogInformation("Checking the page name {0}", pageName);
            _page = _scenarioContext.Get<IPage>("currentPage");
            var currentUrl = _page.Url;
            _logger.LogInformation("Current Url is: {0}", currentUrl);
            currentUrl.Should().Contain(pageName, $"Current url: {currentUrl} does not contain {pageName}");
            
        }

        [When("I fill the checkout form")]
        public async Task WhenIFillTheCheckoutForm(DataTable dataTable)
        {
            var firstRow = dataTable.Rows.FirstOrDefault()
                ?? throw new ArgumentException("Data table should be provided");
            var firstName = firstRow["First Name"];
            var lastName = firstRow["Last Name"];
            var postalCode = firstRow["Postal Code"];
            _logger.LogInformation($"Filling the checkout form with {firstName}, {lastName}, {postalCode}");
          await  _checkoutPage.FillCheckoutForm(firstName, lastName, postalCode); 
        }


        [When("I click the continue button")]
        public async Task WhenIClickTheContinueButton()
        {
            await _checkoutPage.ClickContinueOnCheckout();
        }

        [Then("I click on the finish button")]
        public async Task ThenIClickOnTheFinishButton()
        {
            await _checkoutPage.ClickFinish();
        }

        [Then("I should see the checkout-complete page")]
        public void ThenIShouldSeeTheCheckout_CompletePage()
        {
            var expectedUrl = Endpoints.CheckoutComplete;
            var currentUrl = _page.Url;
            _logger.LogInformation("Current Url is: {0}", currentUrl);
            currentUrl.Should().Contain(expectedUrl, $"Current url: {currentUrl} does not contain {expectedUrl}");
        }
        [Then("I Validate Checkout Second Page")]
        public async Task ThenIValidateCheckoutSecondPage()
        {
           await _checkoutPage.ValidateCheckoutSecondPage();
        }
        [Then("I should see the thank you message")]
        public async Task ThenIShouldSeeTheThankYouMessage()
        {
            await _checkoutCompetePage.ValidateThankYouMessage();
        }
    }
}
 