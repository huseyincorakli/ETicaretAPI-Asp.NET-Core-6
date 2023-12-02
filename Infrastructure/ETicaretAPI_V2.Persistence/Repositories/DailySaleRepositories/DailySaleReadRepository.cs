using ETicaretAPI_V2.Application.Repositories.DailySaleRepositories;
using ETicaretAPI_V2.Domain.Entities;
using ETicaretAPI_V2.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ETicaretAPI_V2.Persistence.Repositories.DailySaleRepositories
{
	public class DailySaleReadRepository : ReadRepository<DailySale>, IDailySaleReadRepository
	{
		ETicaretAPI_V2DBContext dBContext;
		public DailySaleReadRepository(ETicaretAPI_V2DBContext context) : base(context)
		{
			dBContext=context;
		}

		public async Task<List<DailySaleSold>> GetDailySale(DateTime dateTime)
		{
			return await dBContext.Set<DailySaleSold>()
				.FromSqlRaw($"SELECT * FROM get_total_quantity_sold_by_date(p_sale_date := '{dateTime}'::DATE)").ToListAsync();
		}

		
	}
}
