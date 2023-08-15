using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ETicaretAPI_V2.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationService(this IServiceCollection services)
        {
            services.AddMediatR(typeof(ServiceRegistration));
        }
    }
}
