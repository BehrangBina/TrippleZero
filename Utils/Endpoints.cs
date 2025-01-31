using TrippleZero.Common;

namespace TrippleZero.Utils
{
    public abstract class Endpoints
    {
        public static string BaseUrl = EnvironmentManager.GetOrThrow("BaseUrl");
        public static string InventoryPage = $"{BaseUrl}/inventory.html";
    }
}
