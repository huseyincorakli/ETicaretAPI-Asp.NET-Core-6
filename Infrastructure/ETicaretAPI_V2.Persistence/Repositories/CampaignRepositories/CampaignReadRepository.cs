using ETicaretAPI_V2.Application.Repositories.CampaignRepositories;
using ETicaretAPI_V2.Domain.Entities;
using ETicaretAPI_V2.Persistence.Contexts;

namespace ETicaretAPI_V2.Persistence.Repositories.CampaignRepositories
{
	public class CampaignReadRepository : ReadRepository<Campaign>, ICampaignReadRepository
	{
		public CampaignReadRepository(ETicaretAPI_V2DBContext context) : base(context)
		{
		}
	}
}
