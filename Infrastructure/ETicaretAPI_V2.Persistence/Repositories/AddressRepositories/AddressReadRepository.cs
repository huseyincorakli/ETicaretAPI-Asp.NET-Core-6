using ETicaretAPI_V2.Application.Repositories.AddressRepositories;
using ETicaretAPI_V2.Domain.Entities;
using ETicaretAPI_V2.Persistence.Contexts;

namespace ETicaretAPI_V2.Persistence.Repositories.AddressRepositories
{
	public class AddressReadRepository : ReadRepository<Address>, IAddressReadRepository
	{
		public AddressReadRepository(ETicaretAPI_V2DBContext context) : base(context)
		{
		}
	}
}
