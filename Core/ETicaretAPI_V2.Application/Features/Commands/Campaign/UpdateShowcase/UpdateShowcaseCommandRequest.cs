using MediatR;

namespace ETicaretAPI_V2.Application.Features.Commands.Campaign.UpdateShowcase
{
	public class UpdateShowcaseCommandRequest:IRequest<UpdateShowcaseCommandResponse>
	{
		public string  ShowcaseId { get; set; }
		public bool ShowValue { get; set; }
	}
}
