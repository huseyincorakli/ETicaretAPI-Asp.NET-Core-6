using MediatR;
using CC = ETicaretAPI_V2.Application.DTOs.Campaign;

namespace ETicaretAPI_V2.Application.Features.Commands.Campaign.CreateCampaign
{
	public class CreateCampaignCommandRequest:IRequest<CreateCampaignCommandResponse>
	{
		public CC.CreateCampaign CreateCampaign { get; set; }
	}
}
