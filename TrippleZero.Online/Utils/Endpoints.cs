using TrippleZero.Common;

namespace TrippleZero.Online.Utils
{
    /// <summary>
    /// Provides endpoint URLs for various pages in the application.
    /// </summary>
    public abstract class Endpoints
    {
        /// <summary>
        /// The base URL for the application.
        /// </summary>
        public static string BaseUrl = EnvironmentManager.GetOrThrow("BaseUrl");

        /// <summary>
        /// The URL for the inventory page.
        /// </summary>
        public static string InventoryPage = $"{BaseUrl}/inventory.html";

        /// <summary>
        /// The URL for the cart page.
        /// </summary>
        public static string CartPage = $"{BaseUrl}/cart.html";

        /// <summary>
        /// The URL for the first step of the checkout process.
        /// </summary>
        public static string CheckoutPageStep1 = $"{BaseUrl}/checkout-step-one.html";

        /// <summary>
        /// The URL for the second step of the checkout process.
        /// </summary>
        public static string CheckoutPageStep2 = $"{BaseUrl}/checkout-step-two.html";

        /// <summary>
        /// The URL for the checkout completion page.
        /// </summary>
        public static string CheckoutComplete = $"{BaseUrl}/checkout-complete.html";
    }
}
