using MediatR;

namespace ETicaretAPI_V2.Application.Features.Queries.Campaign.GetCampaignById
{
	public class GetCampaignByIdQueryRequest:IRequest<GetCampaignByIdQueryResponse>
	{
		public string CampaignId { get; set; }
	}
}
