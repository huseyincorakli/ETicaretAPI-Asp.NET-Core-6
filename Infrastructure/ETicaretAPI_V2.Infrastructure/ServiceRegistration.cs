using ETicaretAPI_V2.Application.Services;
using ETicaretAPI_V2.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI_V2.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices (this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IFileService, FileService>();
        }
    }
}
