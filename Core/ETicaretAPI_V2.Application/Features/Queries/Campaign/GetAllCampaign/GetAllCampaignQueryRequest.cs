using MediatR;

namespace ETicaretAPI_V2.Application.Features.Queries.Campaign.GetAllCampaign
{
	public class GetAllCampaignQueryRequest:IRequest<GetAllCampaignQueryResponse>
	{
		public int Size { get; set; }=4;
		public string? CampaignCode { get; set; }
	}
}
