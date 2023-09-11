using ETicaretAPI_V2.Application.Abstraction.Services;
using ETicaretAPI_V2.Application.Abstraction.Services.Authentications;
using ETicaretAPI_V2.Application.Repositories.BasketItemRepositories;
using ETicaretAPI_V2.Application.Repositories.BasketRepositories;
using ETicaretAPI_V2.Application.Repositories.CompletedOrderRepositories;
using ETicaretAPI_V2.Application.Repositories.CustomerRepositories;
using ETicaretAPI_V2.Application.Repositories.FileRepositories;
using ETicaretAPI_V2.Application.Repositories.InvoiceFileRepositories;
using ETicaretAPI_V2.Application.Repositories.OrderRepositories;
using ETicaretAPI_V2.Application.Repositories.ProductImageFileRepositories;
using ETicaretAPI_V2.Application.Repositories.ProductRepositories;
using ETicaretAPI_V2.Domain.Entities.Identity;
using ETicaretAPI_V2.Persistence.Contexts;
using ETicaretAPI_V2.Persistence.Repositories.BasketItemRepositories;
using ETicaretAPI_V2.Persistence.Repositories.BasketRepositories;
using ETicaretAPI_V2.Persistence.Repositories.CompletedOrderRepositories;
using ETicaretAPI_V2.Persistence.Repositories.CustomerRepositories;
using ETicaretAPI_V2.Persistence.Repositories.FileRepositories;
using ETicaretAPI_V2.Persistence.Repositories.InvoiceFileRepositories;
using ETicaretAPI_V2.Persistence.Repositories.OrderRepositories;
using ETicaretAPI_V2.Persistence.Repositories.ProductImageFileRepositories;
using ETicaretAPI_V2.Persistence.Repositories.ProductRepositores;
using ETicaretAPI_V2.Persistence.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ETicaretAPI_V2.Persistence
{
    public static class ServiceRegistration
    {

        public static void AddPersistenceService(this IServiceCollection service)
        {
            service.AddDbContext<ETicaretAPI_V2DBContext>(options => options.UseNpgsql(Configuration.ConnectionString));
            service.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit=false;
            }).AddEntityFrameworkStores<ETicaretAPI_V2DBContext>()
            .AddDefaultTokenProviders();


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
            service.AddScoped<IBasketReadRepository,BasketReadRepository>();
            service.AddScoped<IBasketWriteRepository,BasketWriteRepository>();
            service.AddScoped<IBasketItemReadRepository,BasketItemReadRepository>();
            service.AddScoped<IBasketItemWriteRepository,BasketItemWriteRepository>();
            service.AddScoped<ICompletedOrderReadRepository,CompletedOrderReadRepository>();
            service.AddScoped<ICompletedOrderWriteRepository,CompletedOrderWriteRepository>();



            service.AddScoped<IUserService, UserService>();
            service.AddScoped<IAuthService, AuthService>();
            service.AddScoped<IExternalAuthentication, AuthService>();
            service.AddScoped<IInternalAuthentication, AuthService>();
            service.AddScoped<IBasketService, BasketService>();
            service.AddScoped<IOrderService, OrderService>();
        }
    }
}