using ETicaretAPI_V2.Application.Repositories.ShippingCompanyRepositories;
using ETicaretAPI_V2.Domain.Entities;
using ETicaretAPI_V2.Persistence.Contexts;

namespace ETicaretAPI_V2.Persistence.Repositories.ShippingCompanyRepositories
{
	public class ShippingCompanyReadRepository : ReadRepository<ShippingCompany>, IShippingCompanyReadRepository
	{
		public ShippingCompanyReadRepository(ETicaretAPI_V2DBContext context) : base(context)
		{
		}
	}
}
