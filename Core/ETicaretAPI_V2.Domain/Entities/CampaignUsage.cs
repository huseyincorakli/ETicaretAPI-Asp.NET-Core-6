using ETicaretAPI_V2.Domain.Entities.Common;
using ETicaretAPI_V2.Domain.Entities.Identity;

namespace ETicaretAPI_V2.Domain.Entities
{
	public class CampaignUsage :BaseEntitiy
	{
		public string UserId { get; set; }
		public Guid CampaignId { get; set; }
		public DateTime UsageTime { get; set; }

		public AppUser User { get; set; }
		public Campaign Campaign { get; set; }
	}
}
