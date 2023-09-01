using ETicaretAPI_V2.SignalR.Hubs;
using Microsoft.AspNetCore.Builder;

namespace ETicaretAPI_V2.SignalR
{
    public static class HubRegistration
    {
        public static void MapHubs(this WebApplication webApplication)
        {
            webApplication.MapHub<ProductHub>("/products-hub");
        }
    }
}
