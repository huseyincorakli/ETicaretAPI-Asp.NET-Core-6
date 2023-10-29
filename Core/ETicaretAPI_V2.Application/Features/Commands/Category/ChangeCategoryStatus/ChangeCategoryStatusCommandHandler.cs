using ETicaretAPI_V2.Application.Repositories.CategoryRepositories;
using ETicaretAPI_V2.Application.Repositories.CustomerRepositories;
using MediatR;
using ctg = ETicaretAPI_V2.Domain.Entities;

namespace ETicaretAPI_V2.Application.Features.Commands.Category.ChangeCategoryStatus
{
	public class ChangeCategoryStatusCommandHandler : IRequestHandler<ChangeCategoryStatusCommandRequest, ChangeCategoryStatusCommandResponse>
	{
		private readonly ICategoryReadRepository _categoryReadRepository;
		private readonly ICategoryWriteRepository _categoryWriteRepository;

		public ChangeCategoryStatusCommandHandler(ICategoryWriteRepository categoryWriteRepository, ICategoryReadRepository categoryReadRepository)
		{
			_categoryWriteRepository = categoryWriteRepository;
			_categoryReadRepository = categoryReadRepository;
		}

		public async Task<ChangeCategoryStatusCommandResponse> Handle(ChangeCategoryStatusCommandRequest request, CancellationToken cancellationToken)
		{

			ctg.Category category = await _categoryReadRepository.GetByIdAsync(request.CategoryId);
			
				category.IsActive = request.IsActive;
				category.UpdatedDate = DateTime.UtcNow;
				await _categoryWriteRepository.SaveAsync();

				return new()
				{
					IsSucceded=true
				};
			
		}
	}
}
