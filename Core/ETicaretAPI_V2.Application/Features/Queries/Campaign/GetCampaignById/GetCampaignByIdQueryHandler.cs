using ETicaretAPI_V2.Application.Abstraction.Services;
using MediatR;

namespace ETicaretAPI_V2.Application.Features.Queries.Campaign.GetCampaignById
{
	public class GetCampaignByIdQueryHandler : IRequestHandler<GetCampaignByIdQueryRequest, GetCampaignByIdQueryResponse>
	{
		readonly ICampaignService _campaignService;

		public GetCampaignByIdQueryHandler(ICampaignService campaignService)
		{
			_campaignService = campaignService;
		}

		async Task<GetCampaignByIdQueryResponse> IRequestHandler<GetCampaignByIdQueryRequest, GetCampaignByIdQueryResponse>.Handle(GetCampaignByIdQueryRequest request, CancellationToken cancellationToken)
		{
			var data = await _campaignService.GetCampaignByIdAsync(request.CampaignId);
			if (data != null)
			{
				return new()
				{
					Campaign = data
				};
			}
			else
				return new() { Campaign = null };
		}
	}
}
