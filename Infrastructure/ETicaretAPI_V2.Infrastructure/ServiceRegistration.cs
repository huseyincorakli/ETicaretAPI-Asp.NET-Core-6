using ETicaretAPI_V2.Application.Abstraction.Storage;
using ETicaretAPI_V2.Infrastructure.Services;
using ETicaretAPI_V2.Infrastructure.Services.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace ETicaretAPI_V2.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices (this IServiceCollection serviceCollection)
        {
           serviceCollection.AddScoped<IStorageService,StorageService>();
        }
        public static void AddStorage<T> (this IServiceCollection serviceCollection) where T : Storage,IStorage
        {
            serviceCollection.AddScoped<IStorage, T>(); 
        }
    }
}
