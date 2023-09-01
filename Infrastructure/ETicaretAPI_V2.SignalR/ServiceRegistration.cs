using ETicaretAPI.SignalR.HubServices;
using ETicaretAPI_V2.Application.Abstractions.Hubs;
using Microsoft.Extensions.DependencyInjection;

namespace ETicaretAPI_V2.SignalR
{
    public static class ServiceRegistration
    {
        public static void AddSignalRServices(this IServiceCollection collection)
        {
            collection.AddTransient<IProductHubService, ProductHubService>();
            collection.AddSignalR();
        }
    }
}