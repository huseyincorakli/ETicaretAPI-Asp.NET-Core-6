using ETicaretAPI_V2.Application.Repositories.CategoryRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ETicaretAPI_V2.Application.Features.Commands.Category.GetAllCategoryName
{
	public class GetAllCategoryNameCommandHandler : IRequestHandler<GetAllCategoryNameCommandRequest, GetAllCategoryNameCommandResponse>
	{
		private readonly ICategoryReadRepository _categoryReadRepository;

		public GetAllCategoryNameCommandHandler(ICategoryReadRepository categoryReadRepository)
		{
			_categoryReadRepository = categoryReadRepository;
		}

		public async Task<GetAllCategoryNameCommandResponse> Handle(GetAllCategoryNameCommandRequest request, CancellationToken cancellationToken)
		{
			var data = await _categoryReadRepository.GetAll().Where(a => a.IsActive == true).Select(p => new { Id = p.Id.ToString(), p.CategoryName }).ToListAsync();
			List<object> dataList = data.Cast<object>().ToList();

			return new()
			{
				CategoryNames = dataList,
			};
		}
	}
}
