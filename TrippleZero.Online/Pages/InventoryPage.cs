using Microsoft.Playwright;

namespace TrippleZero.Online.Pages
{
    /// <summary>
    /// Represents the inventory page of the application.
    /// </summary>
    internal class InventoryPage
    {
        private readonly IPage _page;
        private readonly ILogger _logger;
        private string ItemContainer = ".inventory_item";
        private string AddToCartButton = "button:has-text('Add to cart')";
        private string RemoveButton = "button:has-text('Remove')";
        private string CartBadge = ".shopping_cart_badge";
        private string CartButton = "#shopping_cart_container";
        private string ItemPrice = ".inventory_item_price";
        private ScenarioContext _scenarioContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryPage"/> class.
        /// </summary>
        /// <param name="page">The Playwright page instance.</param>
        /// <param name="ScenarioContext">The scenario context.</param>
        /// <param name="logger">The logger instance.</param>
        public InventoryPage(IPage page, ScenarioContext ScenarioContext, ILogger logger)
        {
            _page = page;
            _scenarioContext = ScenarioContext;
            _logger = logger;
        }

        /// <summary>
        /// Adds a product to the cart.
        /// </summary>
        /// <param name="productName">The name of the product to add to the cart.</param>
        public async Task AddProductToCart(string productName)
        {
            var productElement = await _page.QuerySelectorAsync($"{ItemContainer}:has-text('{productName}')");

            if (productElement != null)
            {
                var addToCartButton = await productElement.QuerySelectorAsync(AddToCartButton);
                _scenarioContext.Add("productName", productName);
                string price = "";
                var priceElement = await productElement.QuerySelectorAsync(ItemPrice)
                    ?? throw new Exception($"Price for the product {productName} not found");
                price = await priceElement.InnerTextAsync();
                _scenarioContext.Add("productPrice", price);

                if (addToCartButton != null)
                {
                    await addToCartButton.ClickAsync();
                }
            }
        }

        /// <summary>
        /// Checks if the button text changes after clicking 'Add to cart'.
        /// </summary>
        /// <param name="productName">The name of the product to check.</param>
        public async Task CheckProductAddToCardChangedAfterClicking(string productName)
        {
            var querySelector = $"{ItemContainer}:has-text('{productName}')";
            _logger.LogInformation($"Checking if the button text changes after clicking 'Add to cart' for {productName}");
            _logger.LogInformation($"Query Selector: {querySelector}");
            var productElement =
                await _page.QuerySelectorAsync(querySelector
                ?? throw new Exception($"Could not find {productName} with selector {querySelector}"));
            var addToCartButton = await productElement!.QuerySelectorAsync(AddToCartButton);
            addToCartButton.Should().BeNull($" 'Add to cart' button for {productName} should be changed after adding to cart");
            var removeButton = await productElement!.QuerySelectorAsync(RemoveButton);
            removeButton.Should().NotBeNull($" 'Remove' button for {productName} should be visible after adding to cart");
        }

        /// <summary>
        /// Checks if the cart badge shows the correct number of items.
        /// </summary>
        /// <param name="count">The expected number of items in the cart.</param>
        public async Task CheckItemsInCartBadge(int count)
        {
            _logger.LogInformation($"Checking if the cart badge shows {count}");
            _logger.LogInformation($"Cart Badge Query Selector: {CartBadge}");
            var cartBadge = await _page.QuerySelectorAsync(CartBadge);
            cartBadge.Should().NotBeNull("Cart badge should be visible");
            var cartBadgeText = await cartBadge.InnerTextAsync();
            cartBadgeText.Should().Be(count.ToString(), $"Cart badge should show {count}");
        }

        /// <summary>
        /// Clicks the cart button to navigate to the cart page.
        /// </summary>
        public async Task ClickCartButton()
        {
            var cartButton = await _page.QuerySelectorAsync(CartButton);
            cartButton.Should().NotBeNull("Cart button should be visible");
            await cartButton.ClickAsync();
        }
    }
}
