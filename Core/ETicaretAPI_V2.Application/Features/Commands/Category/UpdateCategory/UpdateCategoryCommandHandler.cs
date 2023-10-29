using ETicaretAPI_V2.Application.Repositories.CategoryRepositories;
using MediatR;
using ctg = ETicaretAPI_V2.Domain.Entities;

namespace ETicaretAPI_V2.Application.Features.Commands.Category.UpdateCategory
{
	public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommandRequest, UpdateCategoryCommandResponse>
	{
		private readonly ICategoryReadRepository _categoryReadRepository;
		private readonly ICategoryWriteRepository _categoryWriteRepository;

		public UpdateCategoryCommandHandler(ICategoryReadRepository categoryReadRepository, ICategoryWriteRepository categoryWriteRepository)
		{
			_categoryReadRepository = categoryReadRepository;
			_categoryWriteRepository = categoryWriteRepository;
		}

		public async Task<UpdateCategoryCommandResponse> Handle(UpdateCategoryCommandRequest request, CancellationToken cancellationToken)
		{
			ctg.Category category =  await _categoryReadRepository.GetByIdAsync((request.CategoryId));

			if (category == null) {
				throw new Exception("Category not found!");
			}
			else
			{
				category.CategoryName=request.CategoryName;
				category.IsActive = request.IsActive;
				category.UpdatedDate = DateTime.UtcNow;
				await _categoryWriteRepository.SaveAsync();

			}

			return new();
		}
	}
}
