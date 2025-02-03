using Microsoft.Playwright;

namespace TrippleZero.Online.Pages
{
    /// <summary>
    /// Represents the cart page of the application.
    /// </summary>
    internal class CartPage
    {
        private readonly IPage _page;
        private ScenarioContext _scenarioContext;
        private ILogger _logger;
        private string ItemPrice = "[data-test='inventory-item-price']";
        private string ItemName = "[data-test='inventory-item-name']";
        private string Checkout = "[data-test='checkout']";

        /// <summary>
        /// Initializes a new instance of the <see cref="CartPage"/> class.
        /// </summary>
        /// <param name="page">The Playwright page instance.</param>
        /// <param name="ScenarioContext">The scenario context.</param>
        /// <param name="logger">The logger instance.</param>
        public CartPage(IPage page, ScenarioContext ScenarioContext, ILogger logger)
        {
            _page = page;
            _scenarioContext = ScenarioContext;
            _logger = logger;
        }

        /// <summary>
        /// Checks if the specified product is in the cart.
        /// </summary>
        /// <param name="productName">The name of the product to check.</param>
        internal async Task CheckProductInCart(string productName)
        {
            // Get the item name
            var itemName = await _page.QuerySelectorAsync(ItemName);
            var itemNameText = await itemName!.TextContentAsync();
            _logger.LogInformation($"Item Name: {itemNameText!.Trim()}");
            itemNameText!.Trim().Should().Be(productName, "Item Name should match");
        }

        /// <summary>
        /// Checks the price of the product in the cart.
        /// </summary>
        public async Task CheckProductPrice()
        {
            // Get the item price
            var itemPrice = await _page.QuerySelectorAsync(ItemPrice);
            var itemPriceText = await itemPrice!.TextContentAsync();
            _logger.LogInformation($"Item Price: {itemPriceText!.Trim()}");
            itemPriceText!.Trim().Should().Be(_scenarioContext.Get<string>("productPrice"), "Item Price should match");
        }

        /// <summary>
        /// Checks if the checkout button is visible.
        /// </summary>
        internal async Task CheckCheckoutButton()
        {
            var checkoutButton = await _page.QuerySelectorAsync(Checkout);
            checkoutButton.Should().NotBeNull("Checkout button should be visible");
        }

        /// <summary>
        /// Clicks the checkout button.
        /// </summary>
        internal async Task ClickCheckoutButton()
        {
            var checkoutButton = await _page.QuerySelectorAsync(Checkout);
            await checkoutButton!.ClickAsync();
        }
    }
}

