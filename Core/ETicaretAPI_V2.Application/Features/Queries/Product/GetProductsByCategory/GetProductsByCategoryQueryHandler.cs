using ETicaretAPI_V2.Application.Repositories.ProductRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ETicaretAPI_V2.Application.Features.Queries.Product.GetProductsByCategory
{
	public class GetProductsByCategoryQueryHandler : IRequestHandler<GetProductsByCategoryQueryRequest, GetProductsByCategoryQueryResponse>
	{
		private readonly IProductReadRepository _productReadRepository;

		public GetProductsByCategoryQueryHandler(IProductReadRepository productReadRepository)
		{
			_productReadRepository = productReadRepository;
		}

		public async Task<GetProductsByCategoryQueryResponse> Handle(GetProductsByCategoryQueryRequest request, CancellationToken cancellationToken)
		{
			var data=  await _productReadRepository.GetAll().Where(p => p.CategoryId == Guid.Parse(request.CategoryId)).ToListAsync();
			var dataCount=	data?.Count();

			return new()
			{
				Products = data,
				TotalProductCount = dataCount ?? 0
			};
			
		}
	}
}
