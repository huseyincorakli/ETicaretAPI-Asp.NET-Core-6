using ETicaretAPI.SignalR.HubServices;
using ETicaretAPI_V2.Application.Abstraction.Hubs;
using ETicaretAPI_V2.Application.Abstractions.Hubs;
using ETicaretAPI_V2.SignalR.HubServices;
using Microsoft.Extensions.DependencyInjection;

namespace ETicaretAPI_V2.SignalR
{
    public static class ServiceRegistration
    {
        public static void AddSignalRServices(this IServiceCollection collection)
        {
            collection.AddTransient<IProductHubService, ProductHubService>();
            collection.AddTransient<IOrderHubService, OrderHubService>();
            collection.AddSignalR();
        }
    }
}