using ETicaretAPI_V2.Application.Repositories.ProductTagRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ETicaretAPI_V2.Application.Features.Queries.ProductTag.GetAllProductTag
{
	public class GetAllProductTagQueryHandler : IRequestHandler<GetAllProductTagQueryRequest, GetAllProductTagQueryResponse>
	{
		private readonly IProductTagReadRepository _productTagReadRepository;

		public GetAllProductTagQueryHandler(IProductTagReadRepository productTagReadRepository)
		{
			_productTagReadRepository = productTagReadRepository;
		}

		public async Task<GetAllProductTagQueryResponse> Handle(GetAllProductTagQueryRequest request, CancellationToken cancellationToken)
		{
			var productTagsCount = await _productTagReadRepository.GetAll().CountAsync();
			var productTags = await _productTagReadRepository.GetAll().Skip(request.Page * request.Size).Take(request.Size).Include(x => x.Products).Include(y => y.Category)
				.Select(a => new
				{
					a.Id,
					a.TagName,
					a.IsActive,
					a.CreateDate,
					a.UpdatedDate,
				}).ToListAsync();

			return new()
			{
				ProductTags = productTags,
				TotalProductTagsCount = productTagsCount
			};
		}
	}
}
