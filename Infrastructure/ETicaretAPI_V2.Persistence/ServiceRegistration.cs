
using ETicaretAPI_V2.Application.Repositories.CustomerRepositories;
using ETicaretAPI_V2.Application.Repositories.FileRepositories;
using ETicaretAPI_V2.Application.Repositories.InvoiceFileRepositories;
using ETicaretAPI_V2.Application.Repositories.OrderRepositories;
using ETicaretAPI_V2.Application.Repositories.ProductImageFileRepositories;
using ETicaretAPI_V2.Application.Repositories.ProductRepositories;
using ETicaretAPI_V2.Persistence.Contexts;
using ETicaretAPI_V2.Persistence.Repositories.CustomerRepositories;
using ETicaretAPI_V2.Persistence.Repositories.FileRepositories;
using ETicaretAPI_V2.Persistence.Repositories.InvoiceFileRepositories;
using ETicaretAPI_V2.Persistence.Repositories.OrderRepositories;
using ETicaretAPI_V2.Persistence.Repositories.ProductImageFileRepositories;
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
            service.AddScoped<IFileReadRepository, FileReadRepository>();
            service.AddScoped<IFileWriteRepository,FileWriteRepository>();
            service.AddScoped<IInvoiceFileReadRepository,InvoiceFileReadRepository>();
            service.AddScoped<IInvoiceFileWriteRepository,InvoiceFileWriteRepository>();
            service.AddScoped<IProductImageFileReadRepository,ProductImageFileReadRepository>();
            service.AddScoped<IProductImageFileWriteRepository,ProductImageFileWriteRepository>();
        }
    }
}