
using ETicaretAPI_V2.Application.Repositories.CustomerRepositories;
using ETicaretAPI_V2.Application.Repositories.OrderRepositories;
using ETicaretAPI_V2.Application.Repositories.ProductRepositories;
using ETicaretAPI_V2.Persistence.Contexts;
using ETicaretAPI_V2.Persistence.Repositories.CustomerRepositories;
using ETicaretAPI_V2.Persistence.Repositories.OrderRepositories;
using ETicaretAPI_V2.Persistence.Repositories.ProductRepositores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI_V2.Persistence
{
    public static class ServiceRegistration
    {

        public static void AddPersistenceService(this IServiceCollection service)
        {
            service.AddDbContext<ETicaretAPI_V2DBContext>(options => options.UseNpgsql(Configuration.ConnectionString));
            service.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            service.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
            service.AddScoped<IOrderReadRepository, OrderReadRepository>();
            service.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
            service.AddScoped<IProductReadRepository, ProductReadRepository>();
            service.AddScoped<IProductWriteRepository, ProductWriteRepository>();
        }
    }
}