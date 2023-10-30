using ETicaretAPI_V2.Application.Repositories.CategoryRepositories;
using ETicaretAPI_V2.Application.Repositories.ProductTagRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ctg = ETicaretAPI_V2.Domain.Entities;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ETicaretAPI_V2.Application.Features.Queries.Category.GetCategoryById
{
	internal class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQueryRequest, GetCategoryByIdQueryResponse>
	{
		private readonly ICategoryReadRepository _categoryReadRepository;
		private readonly IProductTagReadRepository _productTagReadRepository;

		
		public GetCategoryByIdQueryHandler(ICategoryReadRepository categoryReadRepository, IProductTagReadRepository productTagReadRepository)
		{
			_categoryReadRepository = categoryReadRepository;
			_productTagReadRepository = productTagReadRepository;
		}

		public async Task<GetCategoryByIdQueryResponse> Handle(GetCategoryByIdQueryRequest request, CancellationToken cancellationToken)
		{
			var options = new JsonSerializerOptions
			{
				ReferenceHandler = ReferenceHandler.IgnoreCycles
			};
			var datas = await _categoryReadRepository.GetByIdAsync(request.CategoryId);
			
			if (datas != null)
			{
				var productTags = await _productTagReadRepository.GetWhere(p => p.CategoryId == Guid.Parse(request.CategoryId)).ToListAsync();
				datas.ProductTags = productTags;
				var datass = JsonSerializer.Serialize(datas, options);
				return new()
				{
					Datas = datass
				};
			}
			else
			{
				throw new Exception("Error");
			}
		}
	}
}
