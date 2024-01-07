using ETicaretAPI_V2.Application.Abstraction.Services;
using ETicaretAPI_V2.Application.Abstraction.Services.Authentications;
using ETicaretAPI_V2.Application.Repositories.AddressRepositories;
using ETicaretAPI_V2.Application.Repositories.BasketItemRepositories;
using ETicaretAPI_V2.Application.Repositories.BasketRepositories;
using ETicaretAPI_V2.Application.Repositories.CampaignRepositories;
using ETicaretAPI_V2.Application.Repositories.CampaignUsageRepositories;
using ETicaretAPI_V2.Application.Repositories.CategoryRepositories;
using ETicaretAPI_V2.Application.Repositories.CommentRepositories;
using ETicaretAPI_V2.Application.Repositories.CompletedOrderRepositories;
using ETicaretAPI_V2.Application.Repositories.CustomerRepositories;
using ETicaretAPI_V2.Application.Repositories.DailySaleRepositories;
using ETicaretAPI_V2.Application.Repositories.EndpointRepositories;
using ETicaretAPI_V2.Application.Repositories.FileRepositories;
using ETicaretAPI_V2.Application.Repositories.HomeSettingRepositories;
using ETicaretAPI_V2.Application.Repositories.InvoiceFileRepositories;
using ETicaretAPI_V2.Application.Repositories.MenuRepositories;
using ETicaretAPI_V2.Application.Repositories.MessageRepositories;
using ETicaretAPI_V2.Application.Repositories.OrderRepositories;
using ETicaretAPI_V2.Application.Repositories.ProductImageFileRepositories;
using ETicaretAPI_V2.Application.Repositories.ProductRepositories;
using ETicaretAPI_V2.Application.Repositories.RefundRepositories;
using ETicaretAPI_V2.Application.Repositories.ShippingCompanyRepositories;
using ETicaretAPI_V2.Domain.Entities.Identity;
using ETicaretAPI_V2.Persistence.Contexts;
using ETicaretAPI_V2.Persistence.Repositories.AddressRepositories;
using ETicaretAPI_V2.Persistence.Repositories.BasketItemRepositories;
using ETicaretAPI_V2.Persistence.Repositories.BasketRepositories;
using ETicaretAPI_V2.Persistence.Repositories.CampaignRepositories;
using ETicaretAPI_V2.Persistence.Repositories.CampaignUsageRepositories;
using ETicaretAPI_V2.Persistence.Repositories.CategoryRepositories;
using ETicaretAPI_V2.Persistence.Repositories.CommentRepositories;
using ETicaretAPI_V2.Persistence.Repositories.CompletedOrderRepositories;
using ETicaretAPI_V2.Persistence.Repositories.CustomerRepositories;
using ETicaretAPI_V2.Persistence.Repositories.DailySaleRepositories;
using ETicaretAPI_V2.Persistence.Repositories.EnpointRepositories;
using ETicaretAPI_V2.Persistence.Repositories.FileRepositories;
using ETicaretAPI_V2.Persistence.Repositories.HomeSettingRepositories;
using ETicaretAPI_V2.Persistence.Repositories.InvoiceFileRepositories;
using ETicaretAPI_V2.Persistence.Repositories.MenuRepositories;
using ETicaretAPI_V2.Persistence.Repositories.MessageRepositories;
using ETicaretAPI_V2.Persistence.Repositories.OrderRepositories;
using ETicaretAPI_V2.Persistence.Repositories.ProductImageFileRepositories;
using ETicaretAPI_V2.Persistence.Repositories.ProductRepositores;
using ETicaretAPI_V2.Persistence.Repositories.RefundRepositories;
using ETicaretAPI_V2.Persistence.Repositories.ShippingCompanyRepositories;
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
				options.Password.RequireDigit = false;
			}).AddEntityFrameworkStores<ETicaretAPI_V2DBContext>()
			.AddDefaultTokenProviders();


			service.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
			service.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
			service.AddScoped<IOrderReadRepository, OrderReadRepository>();
			service.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
			service.AddScoped<IProductReadRepository, ProductReadRepository>();
			service.AddScoped<IProductWriteRepository, ProductWriteRepository>();
			service.AddScoped<IFileReadRepository, FileReadRepository>();
			service.AddScoped<IFileWriteRepository, FileWriteRepository>();
			service.AddScoped<IInvoiceFileReadRepository, InvoiceFileReadRepository>();
			service.AddScoped<IInvoiceFileWriteRepository, InvoiceFileWriteRepository>();
			service.AddScoped<IProductImageFileReadRepository, ProductImageFileReadRepository>();
			service.AddScoped<IProductImageFileWriteRepository, ProductImageFileWriteRepository>();
			service.AddScoped<IBasketReadRepository, BasketReadRepository>();
			service.AddScoped<IBasketWriteRepository, BasketWriteRepository>();
			service.AddScoped<IBasketItemReadRepository, BasketItemReadRepository>();
			service.AddScoped<IBasketItemWriteRepository, BasketItemWriteRepository>();
			service.AddScoped<ICompletedOrderReadRepository, CompletedOrderReadRepository>();
			service.AddScoped<ICompletedOrderWriteRepository, CompletedOrderWriteRepository>();
			service.AddScoped<IMenuReadRepository, MenuReadRepository>();
			service.AddScoped<IMenuWriteRepository, MenuWriteRepository>();
			service.AddScoped<IEndpointReadRepository, EndpointReadRepository>();
			service.AddScoped<IEndpointWriteRepository, EndpointWriteRepository>();
			service.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
			service.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();
			service.AddScoped<IAddressReadRepository, AddressReadRepository>();
			service.AddScoped<IAddressWriteRepository, AddressWriteRepository>();
			service.AddScoped<IDailySaleReadRepository, DailySaleReadRepository>();
			service.AddScoped<IDailySaleWriteRepository, DailySaleWriteRepository>();
			service.AddScoped<ICommentWriteRepository, CommentWriteRepository>();
			service.AddScoped<ICommentReadRepository, CommentReadRepository>();
			service.AddScoped<ICampaignReadRepository, CampaignReadRepository>();
			service.AddScoped<ICampaignWriteRepository, CampaignWriteRepository>();
			service.AddScoped<ICampaignUsageReadRepository, CampaignUsageReadRepository>();
			service.AddScoped<ICampaignUsageWriteRepository, CampaignUsageWriteRepository>();
			service.AddScoped<IShippingCompanyReadRepository, ShippingCompanyReadRepository>();
			service.AddScoped<IShippingCompanyWriteRepository, ShippingCompanyWriteRepository>();
			service.AddScoped<IHomeSettingReadRepositories, HomeSettingReadRepository>();
			service.AddScoped<IHomeSettingWriteRepositories, HomeSettingWriteRepository>();
			service.AddScoped<IMessageReadRepository, MessageReadRepository>();
			service.AddScoped<IMessageWriteRepository, MessageWriteRepository>();
			service.AddScoped<IRefundReadRepository, RefundReadRepository>();
			service.AddScoped<IRefundWriteRepository, RefundWriteRepository>();





			service.AddScoped<IUserService, UserService>();
			service.AddScoped<IAddressService, AddressService>();
			service.AddScoped<IAuthService, AuthService>();
			service.AddScoped<IExternalAuthentication, AuthService>();
			service.AddScoped<IInternalAuthentication, AuthService>();
			service.AddScoped<IBasketService, BasketService>();
			service.AddScoped<IOrderService, OrderService>();
			service.AddScoped<IRoleService, RoleService>();
			service.AddScoped<IAuthorizationEndpointService, AuthorizationEndpointService>();
			service.AddScoped<IProductService, ProductService>();
			service.AddScoped<ICommentService, CommentService>();
			service.AddScoped<ICampaignService, CampaignService>();
			service.AddScoped<IShippingCompanyService, ShippingCompanyService>();
			service.AddScoped<IRefundService, RefundService>();

		}
	}
}