using ETicaretAPI_V2.Domain.Entities.Common;

namespace ETicaretAPI_V2.Domain.Entities
{
	public class Campaign:BaseEntitiy
	{
		public string Title { get; set; }
		public string Code { get; set; }
		public string Content { get; set; }
		public DateTime ExpiredTime { get; set; }
		public bool ShowCase { get; set; } = false;
		public float DiscountPercentage { get; set; }
		public ICollection<CampaignUsage> CampaignUsages { get; set; }
	}
}
