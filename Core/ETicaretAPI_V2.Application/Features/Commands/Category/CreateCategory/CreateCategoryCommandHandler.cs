using ETicaretAPI_V2.Application.Repositories.CategoryRepositories;
using MediatR;

namespace ETicaretAPI_V2.Application.Features.Commands.Category.CreateCategory
{
	public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommandRequest, CreateCategoryCommandResponse>
	{

		private readonly ICategoryWriteRepository _categoryWriteRepository;

		public CreateCategoryCommandHandler(ICategoryWriteRepository categoryWriteRepository)
		{

			_categoryWriteRepository = categoryWriteRepository;
		}

		public async Task<CreateCategoryCommandResponse> Handle(CreateCategoryCommandRequest request, CancellationToken cancellationToken)
		{
			await _categoryWriteRepository.AddAsync(new()
			{
				CategoryName = request.CategoryName,
				CreateDate = DateTime.UtcNow,
				IsActive = request.IsActive,
			});
			await _categoryWriteRepository.SaveAsync();

			return new();

		}
	}
}
