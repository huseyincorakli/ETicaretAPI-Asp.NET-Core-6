using MediatR;

namespace ETicaretAPI_V2.Application.Features.Commands.Campaign.DeleteCampaign
{
	public class DeleteCampaignCommandRequest:IRequest<DeleteCampaignCommandResponse>
	{
		public string CampaignId { get; set; }
	}
}
