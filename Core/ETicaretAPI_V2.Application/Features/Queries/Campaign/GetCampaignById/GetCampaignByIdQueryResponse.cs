using CC = ETicaretAPI_V2.Domain.Entities;

namespace ETicaretAPI_V2.Application.Features.Queries.Campaign.GetCampaignById
{
	public class GetCampaignByIdQueryResponse
	{
		public CC.Campaign Campaign { get; set; }
	}
}
