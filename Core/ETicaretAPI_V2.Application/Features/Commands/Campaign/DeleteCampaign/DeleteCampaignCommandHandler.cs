using ETicaretAPI_V2.Application.Abstraction.Services;
using MediatR;

namespace ETicaretAPI_V2.Application.Features.Commands.Campaign.DeleteCampaign
{
	public class DeleteCampaignCommandHandler : IRequestHandler<DeleteCampaignCommandRequest, DeleteCampaignCommandResponse>
	{
		readonly ICampaignService _campaignService;

		public DeleteCampaignCommandHandler(ICampaignService campaignService)
		{
			_campaignService = campaignService;
		}

		public async Task<DeleteCampaignCommandResponse> Handle(DeleteCampaignCommandRequest request, CancellationToken cancellationToken)
		{
			var data=  await _campaignService.RemoveCampaingAsync(request.CampaignId);
			if (data!=null)
			{
				return new() { IsSuccess = data };
			}
			else
			{
				return new() { IsSuccess = false };
			}
		}
	}
}
