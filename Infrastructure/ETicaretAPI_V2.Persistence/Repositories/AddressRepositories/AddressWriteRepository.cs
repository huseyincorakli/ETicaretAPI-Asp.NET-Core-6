using ETicaretAPI_V2.Application.Repositories.AddressRepositories;
using ETicaretAPI_V2.Domain.Entities;
using ETicaretAPI_V2.Persistence.Contexts;

namespace ETicaretAPI_V2.Persistence.Repositories.AddressRepositories
{
	public class AddressWriteRepository : WriteRepository<Address>, IAddressWriteRepository
	{
		public AddressWriteRepository(ETicaretAPI_V2DBContext context) : base(context)
		{
		}
	}
}
