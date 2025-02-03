using TrippleZero.Common;

namespace TrippleZero.Online.Utils
{
    public abstract class Endpoints
    {
        public static string BaseUrl = EnvironmentManager.GetOrThrow("BaseUrl");
        public static string InventoryPage = $"{BaseUrl}/inventory.html";
        public static string CartPage = $"{BaseUrl}/cart.html";
        public static string CheckoutPageStep1 = $"{BaseUrl}/checkout-step-one.html";
        public static string CheckoutPageStep2 = $"{BaseUrl}/checkout-step-two.html";
        public static string CheckoutComplete = $"{BaseUrl}/checkout-complete.html";
    }
}
