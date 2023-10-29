using MediatR;

namespace ETicaretAPI_V2.Application.Features.Commands.Category.ChangeCategoryStatus
{
	public class ChangeCategoryStatusCommandRequest:IRequest<ChangeCategoryStatusCommandResponse>
	{
		public string CategoryId { get; set; }
		public bool IsActive { get; set; }
	}
}
