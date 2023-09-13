using ETicaretAPI_V2.Application.Abstraction.Services;
using ETicaretAPI_V2.Application.Abstraction.Services.Configurations;
using ETicaretAPI_V2.Application.Abstraction.Storage;
using ETicaretAPI_V2.Application.Abstraction.Token;
using ETicaretAPI_V2.Infrastructure.Services;
using ETicaretAPI_V2.Infrastructure.Services.Configurations;
using ETicaretAPI_V2.Infrastructure.Services.Mail;
using ETicaretAPI_V2.Infrastructure.Services.Storage;
using ETicaretAPI_V2.Infrastructure.Services.Token;
using Microsoft.Extensions.DependencyInjection;

namespace ETicaretAPI_V2.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices (this IServiceCollection serviceCollection)
        {
           serviceCollection.AddScoped<IStorageService,StorageService>();
           serviceCollection.AddScoped<ITokenHandler,TokenHandler>();
           serviceCollection.AddScoped<IMailService,MailService>();
           serviceCollection.AddScoped<IApplicationService,ApplicationService>();
        }
        public static void AddStorage<T> (this IServiceCollection serviceCollection) where T : Storage,IStorage
        {
            serviceCollection.AddScoped<IStorage, T>(); 
        }
    }
}
