using Microsoft.Playwright;
using TrippleZero.Common;
using TrippleZero.Online.Pages;
using TrippleZero.Online.Utils;

namespace TrippleZero.Online.StepDefinitions
{
    /// <summary>
    /// Step definitions for product purchase scenarios.
    /// </summary>
    [Binding]
    public class ProductPurchaseStepDefinitions : TestBase
    {
        private readonly InventoryPage _inventoryPage;
        private readonly CartPage _cartPage;
        private readonly CheckoutPage _checkoutPage;
        private readonly CheckoutCompletePage _checkoutCompetePage;

        private readonly ILogger<LoginStepDefinitions> _logger;
        private static readonly string BrowserType = EnvironmentManager.GetOrThrow("BrowserType");
        private readonly ScenarioContext _scenarioContext;

        private const string CurrentPageKey = "currentPage";
        private const string CurrentUrlKey = "currentUrl";
        private const string ProductNameKey = "productName";
        private const string ProductQtyKey = "productQty";

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductPurchaseStepDefinitions"/> class.
        /// </summary>
        /// <param name="scenarioContext">The scenario context.</param>
        /// <param name="output">The test output helper.</param>
        public ProductPurchaseStepDefinitions(ScenarioContext scenarioContext, ITestOutputHelper output) : base(BrowserType)
        {
            _logger = output.ToLogger<LoginStepDefinitions>();
            _logger.LogInformation($"Browser Type: {BrowserType}");
            _scenarioContext = scenarioContext;

            _inventoryPage = new InventoryPage(_page ?? _scenarioContext.Get<IPage>(CurrentPageKey), _scenarioContext, _logger);
            _cartPage = new CartPage(_page ?? _scenarioContext.Get<IPage>(CurrentPageKey), _scenarioContext, _logger);
            _checkoutPage = new CheckoutPage(_page ?? _scenarioContext.Get<IPage>(CurrentPageKey), _scenarioContext, _logger);
            _checkoutCompetePage = new CheckoutCompletePage(_page ?? _scenarioContext.Get<IPage>(CurrentPageKey), _scenarioContext, _logger);
        }

        /// <summary>
        /// Verifies that the user is on the inventory page.
        /// </summary>
        [Given("I am in the inventory page")]
        public void GivenIAmInTheInventoryPage()
        {
            try
            {
                _logger.LogInformation($"Checking if user is in {Endpoints.InventoryPage}");
                _scenarioContext.Get<string>(CurrentUrlKey).Should().Be(Endpoints.InventoryPage, "Current Url Should Match");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking if user is in the inventory page");
                throw;
            }
        }

        /// <summary>
        /// Adds a product to the cart from the products list.
        /// </summary>
        /// <param name="productName">The name of the product to add to the cart.</param>
        [When("From products list I Add to cart {string}")]
        public async Task WhenFromProductsListIAddToCartAsync(string productName)
        {
            try
            {
                _logger.LogInformation($"Adding {productName} to cart");
                await _inventoryPage.AddProductToCart(productName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while adding {productName} to cart");
                throw;
            }
        }

        /// <summary>
        /// Verifies that the add to cart button changes to remove.
        /// </summary>
        [Then("I add to cart button should change to remove")]
        public async Task ThenIAddToCartButtonShouldChangeToRemoveAsync()
        {
            try
            {
                var productName = _scenarioContext.Get<string>(ProductNameKey);
                await _inventoryPage.CheckProductAddToCardChangedAfterClicking(productName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking if add to cart button changed to remove");
                throw;
            }
        }

        /// <summary>
        /// Verifies the cart badge shows the correct number of items.
        /// </summary>
        /// <param name="itemsInCart">The expected number of items in the cart.</param>
        [Then("I should see the cart badge with {int}")]
        public async Task ThenIShouldSeeTheCartBadgeWith(int itemsInCart)
        {
            try
            {
                await _inventoryPage.CheckItemsInCartBadge(itemsInCart);
                _scenarioContext.Add(ProductQtyKey, itemsInCart);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking the cart badge");
                throw;
            }
        }

        /// <summary>
        /// Clicks the cart button.
        /// </summary>
        [When("I click the cart button")]
        public async Task WhenIClickTheCartButton()
        {
            try
            {
                await _inventoryPage.ClickCartButton();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while clicking the cart button");
                throw;
            }
        }

        /// <summary>
        /// Verifies that the product is in the cart.
        /// </summary>
        [Then("I should see the product in the cart")]
        public async Task ThenIShouldSeeTheProductInTheCartAsync()
        {
            try
            {
                var productName = _scenarioContext.Get<string>(ProductNameKey);
                _logger.LogInformation($"Checking if {productName} is in the cart");
                await _cartPage.CheckProductInCart(productName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking if product is in the cart");
                throw;
            }
        }

        /// <summary>
        /// Verifies that the product price is visible in the cart.
        /// </summary>
        [Then("I should see the product price")]
        public async Task ThenIShouldSeeTheProductPrice()
        {
            try
            {
                _logger.LogInformation($"Checking if price is visible in the cart");
                await _cartPage.CheckProductPrice();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking the product price");
                throw;
            }
        }

        /// <summary>
        /// Verifies that the checkout button is visible in the cart.
        /// </summary>
        [Then("I should see the checkout button")]
        public async Task ThenIShouldSeeTheCheckoutButton()
        {
            try
            {
                _logger.LogInformation($"Checking if checkout button is visible in the cart");
                await _cartPage.CheckCheckoutButton();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking the checkout button");
                throw;
            }
        }

        /// <summary>
        /// Clicks the checkout button.
        /// </summary>
        [When("I click the checkout button")]
        public async Task WhenIClickTheCheckoutButton()
        {
            try
            {
                _logger.LogInformation("Click on Checkout button");
                await _cartPage.ClickCheckoutButton();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while clicking the checkout button");
                throw;
            }
        }

        /// <summary>
        /// Verifies that the user is on the specified page.
        /// </summary>
        /// <param name="pageName">The name of the page to verify.</param>
        [Then("I am on {string} page")]
        public void ThenIAmOnPage(string pageName)
        {
            try
            {
                _logger.LogInformation("Checking the page name {0}", pageName);
                _page = _scenarioContext.Get<IPage>(CurrentPageKey);
                var currentUrl = _page.Url;
                _logger.LogInformation("Current Url is: {0}", currentUrl);
                currentUrl.Should().Contain(pageName, $"Current url: {currentUrl} does not contain {pageName}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while checking if on {pageName} page");
                throw;
            }
        }

        /// <summary>
        /// Fills the checkout form with the provided data.
        /// </summary>
        /// <param name="dataTable">The data table containing the checkout form data.</param>
        [When("I fill the checkout form")]
        public async Task WhenIFillTheCheckoutForm(DataTable dataTable)
        {
            try
            {
                var firstRow = dataTable.Rows.FirstOrDefault()
                    ?? throw new ArgumentException("Data table should be provided");
                var firstName = firstRow["First Name"];
                var lastName = firstRow["Last Name"];
                var postalCode = firstRow["Postal Code"];
                _logger.LogInformation($"Filling the checkout form with {firstName}, {lastName}, {postalCode}");
                await _checkoutPage.FillCheckoutForm(firstName, lastName, postalCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while filling the checkout form");
                throw;
            }
        }

        /// <summary>
        /// Clicks the continue button on the checkout page.
        /// </summary>
        [When("I click the continue button")]
        public async Task WhenIClickTheContinueButton()
        {
            try
            {
                await _checkoutPage.ClickContinueOnCheckout();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while clicking the continue button");
                throw;
            }
        }

        /// <summary>
        /// Clicks the finish button on the checkout page.
        /// </summary>
        [Then("I click on the finish button")]
        public async Task ThenIClickOnTheFinishButton()
        {
            try
            {
                await _checkoutPage.ClickFinish();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while clicking the finish button");
                throw;
            }
        }

        /// <summary>
        /// Verifies that the user is on the checkout-complete page.
        /// </summary>
        [Then("I should see the checkout-complete page")]
        public void ThenIShouldSeeTheCheckout_CompletePage()
        {
            try
            {
                var expectedUrl = Endpoints.CheckoutComplete;
                var currentUrl = _page.Url;
                _logger.LogInformation("Current Url is: {0}", currentUrl);
                currentUrl.Should().Contain(expectedUrl, $"Current url: {currentUrl} does not contain {expectedUrl}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking the checkout-complete page");
                throw;
            }
        }

        /// <summary>
        /// Validates the second page of the checkout process.
        /// </summary>
        [Then("I Validate Checkout Second Page")]
        public async Task ThenIValidateCheckoutSecondPage()
        {
            try
            {
                await _checkoutPage.ValidateCheckoutSecondPage();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while validating the checkout second page");
                throw;
            }
        }

        /// <summary>
        /// Verifies that the thank you message is displayed on the checkout-complete page.
        /// </summary>
        [Then("I should see the thank you message")]
        public async Task ThenIShouldSeeTheThankYouMessage()
        {
            try
            {
                await _checkoutCompetePage.ValidateThankYouMessage();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking the thank you message");
                throw;
            }
        }
    }
}
