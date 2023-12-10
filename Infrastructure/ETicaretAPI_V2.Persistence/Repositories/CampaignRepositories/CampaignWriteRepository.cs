using ETicaretAPI_V2.Application.Repositories.CampaignRepositories;
using ETicaretAPI_V2.Domain.Entities;
using ETicaretAPI_V2.Persistence.Contexts;

namespace ETicaretAPI_V2.Persistence.Repositories.CampaignRepositories
{
	public class CampaignWriteRepository : WriteRepository<Campaign>, ICampaignWriteRepository
	{
		public CampaignWriteRepository(ETicaretAPI_V2DBContext context) : base(context)
		{
		}
	}
}
