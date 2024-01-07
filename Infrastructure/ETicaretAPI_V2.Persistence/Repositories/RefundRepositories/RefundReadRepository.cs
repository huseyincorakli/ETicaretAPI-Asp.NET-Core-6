using ETicaretAPI_V2.Application.Repositories.RefundRepositories;
using ETicaretAPI_V2.Domain.Entities;
using ETicaretAPI_V2.Persistence.Contexts;

namespace ETicaretAPI_V2.Persistence.Repositories.RefundRepositories
{
	public class RefundReadRepository : ReadRepository<Refund>, IRefundReadRepository
	{
		public RefundReadRepository(ETicaretAPI_V2DBContext context) : base(context)
		{
		}
	}
}
