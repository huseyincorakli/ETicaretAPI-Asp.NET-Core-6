using ETicaretAPI_V2.Application.Abstraction.Services;
using MediatR;

namespace ETicaretAPI_V2.Application.Features.Commands.Campaign.UpdateShowcase
{
	public class UpdateShowcaseCommandHandler : IRequestHandler<UpdateShowcaseCommandRequest, UpdateShowcaseCommandResponse>
	{
		readonly ICampaignService _campaignService;

		public UpdateShowcaseCommandHandler(ICampaignService campaignService)
		{
			_campaignService = campaignService;
		}

		async Task<UpdateShowcaseCommandResponse> IRequestHandler<UpdateShowcaseCommandRequest, UpdateShowcaseCommandResponse>.Handle(UpdateShowcaseCommandRequest request, CancellationToken cancellationToken)
		{
			bool response = await _campaignService.UpdateShowcaseAsync(request.ShowcaseId, request.ShowValue);
			return new()
			{
				IsSuccess = response
			};
		}
	}
}
