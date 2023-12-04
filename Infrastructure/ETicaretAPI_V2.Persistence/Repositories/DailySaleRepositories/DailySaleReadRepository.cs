using ETicaretAPI_V2.Application.Repositories.DailySaleRepositories;
using ETicaretAPI_V2.Domain.Entities;
using ETicaretAPI_V2.Persistence.Contexts;

namespace ETicaretAPI_V2.Persistence.Repositories.DailySaleRepositories
{
	public class DailySaleReadRepository : ReadRepository<DailySale>, IDailySaleReadRepository
	{
		public DailySaleReadRepository(ETicaretAPI_V2DBContext context) : base(context)
		{
		}
	}
}
