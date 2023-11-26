using ETicaretAPI_V2.Application.Repositories.ProductRepositories;
using ETicaretAPI_V2.Domain.Entities;
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
			var data=  await _productReadRepository.GetAll(false).Where(p => p.CategoryId == Guid.Parse(request.CategoryId)).Include(p=>p.ProductImageFiles).Select(p => new
			{
				p.Id,
				p.Name,
				p.Stock,
				p.Price,
				p.CreateDate,
				p.UpdatedDate,
				p.ProductImageFiles,
				p.Category.CategoryName,
				p.Brand,
				p.Specifications
			})
				.ToListAsync(); 

			var dataCount=	data?.Count();
			if (!string.IsNullOrEmpty(request.ProductName))
			{
				data = data.Where(p => p.Name.Contains(request.ProductName, StringComparison.OrdinalIgnoreCase)).ToList();
			}
			return new()
			{
				Products = data,
				TotalProductCount = dataCount ?? 0
			};
			
		}
	}
}
