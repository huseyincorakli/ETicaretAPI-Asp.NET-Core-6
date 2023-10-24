using MediatR;

namespace ETicaretAPI_V2.Application.Features.Commands.Product.CreateProductDescription
{
	public class CreateProductDescriptionCommandRequest:IRequest<CreateProductDescriptionCommandResponse>
	{
		public string Brand { get; set; }
		public string Category { get; set; }
		public string Description { get; set; }
		public string[] Keywords { get; set; }
		public string  Name { get; set; }
		
	}
}
