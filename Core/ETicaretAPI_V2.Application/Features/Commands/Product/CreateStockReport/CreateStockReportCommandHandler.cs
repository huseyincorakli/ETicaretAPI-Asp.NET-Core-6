using ETicaretAPI_V2.Application.Abstraction.Storage;
using ETicaretAPI_V2.Application.Repositories.ProductRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ETicaretAPI_V2.Application.Features.Commands.Product.CreateStockReport
{
	public class CreateStockReportCommandHandler : IRequestHandler<CreateStockReportCommandRequest, CreateStockReportCommandResponse>
	{
		readonly IProductReadRepository _productReadRepository;
		public CreateStockReportCommandHandler(IStorageService storageService, IProductReadRepository productReadRepository)
		{
			_productReadRepository = productReadRepository;
		}

		public async Task<CreateStockReportCommandResponse> Handle(CreateStockReportCommandRequest request, CancellationToken cancellationToken)
		{
		  var data = await	_productReadRepository.GetAll().OrderBy(p => p.Stock).Take(request.Size).Select(a=> new
			{
				a.Brand,
				a.Name,
				a.Stock
			}).ToListAsync();

			return new CreateStockReportCommandResponse()
			{
				ProductStocks = data,
			};
		}
	}
}
