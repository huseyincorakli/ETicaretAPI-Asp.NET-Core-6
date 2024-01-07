using ETicaretAPI_V2.Application.Repositories.RefundRepositories;
using ETicaretAPI_V2.Domain.Entities;
using ETicaretAPI_V2.Persistence.Contexts;

namespace ETicaretAPI_V2.Persistence.Repositories.RefundRepositories
{
	public class RefundWriteRepository : WriteRepository<Refund>, IRefundWriteRepository
	{
		public RefundWriteRepository(ETicaretAPI_V2DBContext context) : base(context)
		{
		}
	}
}
