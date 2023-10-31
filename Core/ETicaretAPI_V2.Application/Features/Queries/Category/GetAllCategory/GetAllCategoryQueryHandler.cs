using ETicaretAPI_V2.Application.Repositories.CategoryRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ETicaretAPI_V2.Application.Features.Queries.Category.GetAllCategory
{
	public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQueryRequest, GetAllCategoryQueryrResponse>
	{
		private readonly ICategoryReadRepository _categoryReadRepository;

		public GetAllCategoryQueryHandler(ICategoryReadRepository categoryReadRepository)
		{
			_categoryReadRepository = categoryReadRepository;
		}

		public async Task<GetAllCategoryQueryrResponse> Handle(GetAllCategoryQueryRequest request, CancellationToken cancellationToken)
		{
			int categoryCount =  await _categoryReadRepository.GetAll(false).CountAsync();
			var categories = await _categoryReadRepository.GetAll(false)
				.Skip(request.Page*request.Size)
				.Take(request.Size)
				
				.Select(x => new
				{
					x.Id,
					x.CategoryName,
					x.IsActive,
					x.CreateDate,
					x.UpdatedDate,
					
					
				})
				.ToListAsync();

			return new()
			{
				Categories = categories,
				TotalCategoryCount = categoryCount
			};

		}
	}
}
