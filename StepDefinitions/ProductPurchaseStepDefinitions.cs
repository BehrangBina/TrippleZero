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
        private ILogger<LoginSteps> _logger;
        private static string _browserType = EnvironmentManager.GetOrThrow("BrowserType");
        private ScenarioContext _scenarioContext;
        public ProductPurchaseStepDefinitions(ScenarioContext scenarioContext, ITestOutputHelper output) : base(_browserType) // You can change "chromium" to any browser type you want to use
        {
            _logger = output.ToLogger<LoginSteps>();
            _logger.LogInformation($"Browser Type: {_browserType}");           
            _scenarioContext = scenarioContext;
            _inventoryPage = new InventoryPage(_page ?? _scenarioContext.Get<IPage>("currentPage"),_scenarioContext,_logger);
            _cartPage = new CartPage(_page ?? _scenarioContext.Get<IPage>("currentPage"), _scenarioContext, _logger);

        }
 

        [Given("I am in the inventory page")]
        public void GivenIAmInTheInventoryPage()
        {
            _logger.LogInformation($"Checking if user is in {Endpoints.InventoryPage}");
            _scenarioContext.Get<string>("currentUrl").Should().Be(Endpoints.InventoryPage,"Current Url Should Match");
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
        public void ThenIAmOnPage(string p0)
        {
            throw new PendingStepException();
        }

        [When("I fill the checkout form")]
        public void WhenIFillTheCheckoutForm()
        {
            throw new PendingStepException();
        }

        [When("I click the continue button")]
        public void WhenIClickTheContinueButton()
        {
            throw new PendingStepException();
        }

        [Then("I click on the finish button")]
        public void ThenIClickOnTheFinishButton()
        {
            throw new PendingStepException();
        }

        [Then("I should see the checkout-complete page")]
        public void ThenIShouldSeeTheCheckout_CompletePage()
        {
            throw new PendingStepException();
        }
    }
}
//cart.html