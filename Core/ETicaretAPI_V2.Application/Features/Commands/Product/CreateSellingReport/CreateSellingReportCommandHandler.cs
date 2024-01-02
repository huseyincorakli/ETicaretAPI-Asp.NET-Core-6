using ETicaretAPI_V2.Application.Repositories.ProductRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ETicaretAPI_V2.Application.Features.Commands.Product.CreateSellingReport
{
	public class CreateSellingReportCommandHandler : IRequestHandler<CreateSellingReportCommandRequest, CreateSellingReportCommandResponse>
	{
		readonly IProductReadRepository _productReadRepository;

		public CreateSellingReportCommandHandler(IProductReadRepository productReadRepository)
		{
			_productReadRepository = productReadRepository;
		}

		public async Task<CreateSellingReportCommandResponse> Handle(CreateSellingReportCommandRequest request, CancellationToken cancellationToken)
		{
		 var data =	await _productReadRepository.GetAll().OrderByDescending(a => a.QuantitySold).Take(request.Size).Select(x => new
			{
				x.Id,
				x.Brand,
				x.Name,
				x.Price,
				x.QuantitySold,
				x.ProductImageFiles
			}).ToListAsync();

			return new()
			{
				ProductSellings = data,
			};
		}
	}
}
