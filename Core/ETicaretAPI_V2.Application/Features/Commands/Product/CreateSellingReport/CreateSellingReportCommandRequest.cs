using MediatR;

namespace ETicaretAPI_V2.Application.Features.Commands.Product.CreateSellingReport
{
	public class CreateSellingReportCommandRequest:IRequest<CreateSellingReportCommandResponse>
	{
		public int Size { get; set; } = 20;
	}
}
