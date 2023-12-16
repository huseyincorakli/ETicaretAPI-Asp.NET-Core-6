using ETicaretAPI_V2.Application.Repositories.ShippingCompanyRepositories;
using ETicaretAPI_V2.Domain.Entities;
using ETicaretAPI_V2.Persistence.Contexts;

namespace ETicaretAPI_V2.Persistence.Repositories.ShippingCompanyRepositories
{
	public class ShippingCompanyWriteRepository : WriteRepository<ShippingCompany>, IShippingCompanyWriteRepository
	{
		public ShippingCompanyWriteRepository(ETicaretAPI_V2DBContext context) : base(context)
		{
		}
	}
}
