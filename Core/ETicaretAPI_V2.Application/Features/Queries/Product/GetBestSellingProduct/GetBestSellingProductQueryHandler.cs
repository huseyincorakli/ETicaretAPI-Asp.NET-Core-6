using ETicaretAPI_V2.Application.Repositories.ProductRepositories;
using ETicaretAPI_V2.Application.ViewModels.Products;
using ETicaretAPI_V2.Domain.Entities;
using MediatR;

namespace ETicaretAPI_V2.Application.Features.Queries.Product.GetBestSellingProduct
{
	public class GetBestSellingProductQueryHandler : IRequestHandler<GetBestSellingProductQueryRequest, GetBestSellingProductQueryResponse>
	{
		readonly IProductReadRepository _productReadRepository;

		public GetBestSellingProductQueryHandler(IProductReadRepository productReadRepository)
		{
			_productReadRepository = productReadRepository;
		}

		public async Task<GetBestSellingProductQueryResponse> Handle(GetBestSellingProductQueryRequest request, CancellationToken cancellationToken)
		{
			var data = await _productReadRepository.GetSellingProductsAsync();

			var viewData = data.Select(product => new VM_Get_Best_Selling_Products
			{
				Id = product.Id,
				Name = product.Name,
				QuantitySold = product.QuantitySold
			}).ToList();

			return new()
			{
				BestSellingProducts = viewData
			};
		}
	}
}
