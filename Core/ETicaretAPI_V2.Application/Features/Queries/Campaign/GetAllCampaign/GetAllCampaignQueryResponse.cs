using CC = ETicaretAPI_V2.Domain.Entities;
namespace ETicaretAPI_V2.Application.Features.Queries.Campaign.GetAllCampaign
{
	public class GetAllCampaignQueryResponse
	{
		public List<CC.Campaign> Campaigns { get; set; }
	}
}
