using ETicaretAPI_V2.Domain.Entities;

namespace ETicaretAPI_V2.Application.Repositories.DailySaleRepositories
{
	public interface IDailySaleReadRepository:IReadRepository<DailySale>
	{
		Task<List<DailySaleSold>> GetDailySale(DateTime dateTime);
	}
}
