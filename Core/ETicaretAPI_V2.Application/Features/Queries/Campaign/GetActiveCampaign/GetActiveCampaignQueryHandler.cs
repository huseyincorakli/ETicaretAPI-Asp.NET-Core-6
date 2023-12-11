using ETicaretAPI_V2.Application.Abstraction.Services;
using MediatR;

namespace ETicaretAPI_V2.Application.Features.Queries.Campaign.GetActiveCampaign
{
	internal class GetActiveCampaignQueryHandler : IRequestHandler<GetActiveCampaignQueryRequest, GetActiveCampaignQueryResponse>
	{

		readonly ICampaignService _campaignService;

		public GetActiveCampaignQueryHandler(ICampaignService campaignService)
		{
			_campaignService = campaignService;
		}

		public async Task<GetActiveCampaignQueryResponse> Handle(GetActiveCampaignQueryRequest request, CancellationToken cancellationToken)
		{
			var data = await _campaignService.GetActiveCampaign();
			if (data!=null)
			{
				return new()
				{
					Campaign = data
				};

			}
			else
			{
				return new GetActiveCampaignQueryResponse()
				{
					Campaign = null
				};
			}
		}
	}
}
