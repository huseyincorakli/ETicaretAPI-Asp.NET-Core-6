using ETicaretAPI_V2.Application.Abstraction.Services;
using MediatR;

namespace ETicaretAPI_V2.Application.Features.Commands.Campaign.CreateCampaign
{
	public class CreateCampaignCommandHandler : IRequestHandler<CreateCampaignCommandRequest, CreateCampaignCommandResponse>
	{
		readonly ICampaignService _campaignService;

		public CreateCampaignCommandHandler(ICampaignService campaignService)
		{
			_campaignService = campaignService;
		}

		public async Task<CreateCampaignCommandResponse> Handle(CreateCampaignCommandRequest request, CancellationToken cancellationToken)
		{
			var data =	await _campaignService.CreatCampaignAsync(request.CreateCampaign);
			if (data==true)
			{
				return new()
				{
					isSuccess=true
				};
			}
			else
				return new() { isSuccess=false };
		}
	}
}
