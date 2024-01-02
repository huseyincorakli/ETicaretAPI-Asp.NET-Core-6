using ETicaretAPI_V2.Application.Repositories.HomeSettingRepositories;
using ETicaretAPI_V2.Domain.Entities;
using ETicaretAPI_V2.Persistence.Contexts;

namespace ETicaretAPI_V2.Persistence.Repositories.HomeSettingRepositories
{
	public class HomeSettingWriteRepository : WriteRepository<HomeSetting>, IHomeSettingWriteRepositories
	{
		public HomeSettingWriteRepository(ETicaretAPI_V2DBContext context) : base(context)
		{
		}
	}
}
