namespace ETicaretAPI_V2.Application.DTOs.Campaign
{
	public class CreateCampaign
	{
		public string Title { get; set; }
		public string Code { get; set; }
		public string Content { get; set; }
		public DateTime ExpiredTime { get; set; }
		public bool Showcase { get; set; }=false;
		public float DiscountPercentage { get; set; }

	}
}
