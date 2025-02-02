using Microsoft.Playwright;

namespace TrippleZero.Pages
{
    internal class CartPage
    {
        private readonly IPage _page;
        private ScenarioContext _scenarioContext;
        private ILogger _logger;
        private string ItemPrice = "[data-test='inventory-item-price']";
        private string ItemName = "[data-test='inventory-item-name']";
        private string Checkoout = "[data-test='checkout']";


        public CartPage(IPage page, ScenarioContext ScenarioContext, ILogger logger)
        {
            _page = page;
            _scenarioContext = ScenarioContext;
            _logger = logger;
        }

        internal async Task CheckProductInCart(string productName)
        {
            // Get the item name
            var itemName = await _page.QuerySelectorAsync(ItemName);
            var itemNameText = await itemName!.TextContentAsync();
            _logger.LogInformation($"Item Name: {itemNameText!.Trim()}");
            itemNameText!.Trim().Should().Be(productName, "Item Name should match");
        }
        public async Task CheckProductPrice()
        {
            // Get the item price
            var itemPrice = await _page.QuerySelectorAsync(ItemPrice);
            var itemPriceText = await itemPrice!.TextContentAsync();
            _logger.LogInformation($"Item Price: {itemPriceText!.Trim()}");
            itemPriceText!.Trim().Should().Be(_scenarioContext.Get<string>("productPrice"), "Item Price should match");
        }

        internal async Task CheckCheckoutButton()
        {
            var checkoutButton = await _page.QuerySelectorAsync(Checkoout);
            checkoutButton.Should().NotBeNull("Checkout button should be visible");
        }

        internal async Task ClickCheckoutButton()
        {
            var checkoutButton = await _page.QuerySelectorAsync(Checkoout);
            await checkoutButton!.ClickAsync();
        }
    }
}

