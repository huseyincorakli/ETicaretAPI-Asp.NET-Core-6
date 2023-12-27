using MediatR;

namespace ETicaretAPI_V2.Application.Features.Commands.Product.CreateStockReport
{
	public class CreateStockReportCommandRequest:IRequest<CreateStockReportCommandResponse>
	{
		public int Size { get; set; } = 20;
	}
}
