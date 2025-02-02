using Microsoft.Playwright;
using TrippleZero.StepDefinitions;

namespace TrippleZero.Pages
{
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

        public InventoryPage(IPage page, ScenarioContext ScenarioContext, ILogger logger)
        {
            _page = page;
            _scenarioContext = ScenarioContext;
            _logger = logger;
        }

        public async Task AddProductToCart(string productName)
        {
            var productElement = await _page.QuerySelectorAsync($"{ItemContainer}:has-text('{productName}')");

            if (productElement != null)
            {
                var addToCartButton = await productElement.QuerySelectorAsync(AddToCartButton);
                _scenarioContext.Add("productName", productName);
                string price = "";
                var priceElement = await productElement.QuerySelectorAsync(ItemPrice)
                    ?? throw new Exception($"Prie for the product {productName}");
                price = await priceElement.InnerTextAsync();
                _scenarioContext.Add("productPrice", price);

                if (addToCartButton != null)
                {
                    await addToCartButton.ClickAsync();
                }
            }
        }
        public async Task CheckProductAddToCardChangedAfterClicking(string productName)
        {
            var querySelector = $"{ItemContainer}:has-text('{productName}')";
            _logger.LogInformation($"Checking if the button text changes after clicking 'Add to cart' for {productName}");
            _logger.LogInformation($"Query Selector: {querySelector}");
            var productElement =
                await _page.QuerySelectorAsync(querySelector
                ?? throw new Exception($"Could not find {productName} with selector {querySelector}"));
            var addToCartButton = await productElement!.QuerySelectorAsync(AddToCartButton);
            addToCartButton.Should().BeNull($" 'Add to cart' button for {productName} should be changed after addeing to cart");
            var removeButton = await productElement!.QuerySelectorAsync(RemoveButton);
            removeButton.Should().NotBeNull($" 'Remove' button for {productName} should be visible after addeing to cart");
        }
        public async Task CheckItemsInCartBadge(int count, ILogger<LoginStepDefinitions> _logger)
        {
            _logger.LogInformation($"Checking if the cart badge shows {count}");
            _logger.LogInformation($"Cart Badge Query Selector: {CartBadge}");
            var cartBadge = await _page.QuerySelectorAsync(CartBadge);
            cartBadge.Should().NotBeNull("Cart badge should be visible");
            var cartBadgeText = await cartBadge.InnerTextAsync();
            cartBadgeText.Should().Be(count.ToString(), $"Cart badge should show {count}");
        }
        public async Task ClickCartButton()
        {
            var cartButton = await _page.QuerySelectorAsync(CartButton);
            cartButton.Should().NotBeNull("Cart button should be visible");
            await cartButton.ClickAsync();
        }
    }
}
