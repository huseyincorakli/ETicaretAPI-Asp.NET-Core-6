using ETicaretAPI_V2.Application.Repositories.ProductRepositories;
using ETicaretAPI_V2.Application.ViewModels.Products;
using MediatR;

namespace ETicaretAPI_V2.Application.Features.Queries.Product.GetLowStockProduct
{
	public class GetLowStockProductQueryHandler : IRequestHandler<GetLowStockProductQueryRequest, GetLowStockProductQueryResponse>
	{
		readonly IProductReadRepository _productReadRepository;

		public GetLowStockProductQueryHandler(IProductReadRepository productReadRepository)
		{
			_productReadRepository = productReadRepository;
		}

		public async Task<GetLowStockProductQueryResponse> Handle(GetLowStockProductQueryRequest request, CancellationToken cancellationToken)
		{
			var data = await _productReadRepository.GetTop5LowestStockProductsAsync();

			var viewModelList = data.Select(product => new VM_Get_Low_Stock_Products
			{
				Id = product.Id,
				Name = product.Name,
				Stock = product.Stock
			}).ToList();

			return new GetLowStockProductQueryResponse
			{
				LowStockProducts = viewModelList
			};
		}
	}
}
