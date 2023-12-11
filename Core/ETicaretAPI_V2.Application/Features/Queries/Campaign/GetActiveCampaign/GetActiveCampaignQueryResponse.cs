using CC = ETicaretAPI_V2.Domain.Entities;
namespace ETicaretAPI_V2.Application.Features.Queries.Campaign.GetActiveCampaign
{
	public class GetActiveCampaignQueryResponse
	{
		public CC.Campaign Campaign { get; set; }
	}
}
