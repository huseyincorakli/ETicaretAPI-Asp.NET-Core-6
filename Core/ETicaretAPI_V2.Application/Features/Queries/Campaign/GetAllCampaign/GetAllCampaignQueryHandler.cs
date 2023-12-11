using ETicaretAPI_V2.Application.Abstraction.Services;
using MediatR;

namespace ETicaretAPI_V2.Application.Features.Queries.Campaign.GetAllCampaign
{
	public class GetAllCampaignQueryHandler : IRequestHandler<GetAllCampaignQueryRequest, GetAllCampaignQueryResponse>
	{
		readonly ICampaignService _campaignService;

		public GetAllCampaignQueryHandler(ICampaignService campaignService)
		{
			_campaignService = campaignService;
		}

		public async Task<GetAllCampaignQueryResponse> Handle(GetAllCampaignQueryRequest request, CancellationToken cancellationToken)
		{
			var data = await _campaignService.GetAllCampaignAsync();

			if (data != null)
			{
				return new() { Campaigns = data };
			}
			else
				return null;
		}
	}
}
