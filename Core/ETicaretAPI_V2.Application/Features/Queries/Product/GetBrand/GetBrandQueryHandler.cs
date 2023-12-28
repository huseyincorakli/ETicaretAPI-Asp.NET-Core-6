using ETicaretAPI_V2.Application.Repositories.ProductRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ETicaretAPI_V2.Application.Features.Queries.Product.GetBrand
{
	public class GetBrandQueryHandler : IRequestHandler<GetBrandQueryRequest, GetBrandQueryResponse>
	{
		readonly IProductReadRepository _productReadRepository;

		public GetBrandQueryHandler(IProductReadRepository productReadRepository)
		{
			_productReadRepository = productReadRepository;
		}

		public async Task<GetBrandQueryResponse> Handle(GetBrandQueryRequest request, CancellationToken cancellationToken)
		{
			var data = await _productReadRepository.GetAll().Select(a => new
			{
				Name= a.Brand
			}).ToListAsync();

			return new()
			{
				Brands = data
			};
		}
	}
}
