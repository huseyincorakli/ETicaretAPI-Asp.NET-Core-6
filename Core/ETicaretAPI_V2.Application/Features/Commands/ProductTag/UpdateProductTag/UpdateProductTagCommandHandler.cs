using ETicaretAPI_V2.Application.Repositories.CategoryRepositories;
using ETicaretAPI_V2.Application.Repositories.ProductTagRepositories;
using MediatR;
using pt = ETicaretAPI_V2.Domain.Entities;
namespace ETicaretAPI_V2.Application.Features.Commands.ProductTag.UpdateProductTag
{
	public class UpdateProductTagCommandHandler : IRequestHandler<UpdateProductTagCommandRequest, UpdateProductTagCommandResponse>
	{
		private readonly IProductTagReadRepository _productTagReadRepository;
		private readonly IProductTagWriteRepository _productTagWriteRepository;
		private readonly ICategoryReadRepository _categoryReadRepository;
		public UpdateProductTagCommandHandler(IProductTagReadRepository productTagReadRepository, IProductTagWriteRepository productTagWriteRepository, ICategoryReadRepository categoryReadRepository)
		{
			_productTagReadRepository = productTagReadRepository;
			_productTagWriteRepository = productTagWriteRepository;
			_categoryReadRepository = categoryReadRepository;
		}

		public async Task<UpdateProductTagCommandResponse> Handle(UpdateProductTagCommandRequest request, CancellationToken cancellationToken)
		{

			pt.ProductTag? productTag = await _productTagReadRepository.GetByIdAsync(request.ProductTagId);
			if (productTag != null)
			{
				productTag.TagName = request.TagName;
				if (request.CategoryId == "")
					productTag.CategoryId = productTag.CategoryId;
				else
				{
					var category = await _categoryReadRepository.GetByIdAsync(request.CategoryId);
					if (category!=null)
					{
						productTag.CategoryId = Guid.Parse(request.CategoryId);
					}
					else
					{
						productTag.CategoryId = productTag.CategoryId;
					}

				}
				productTag.UpdatedDate = DateTime.UtcNow;
				productTag.IsActive = request.IsActive;

				await _productTagWriteRepository.SaveAsync();

				return new();
			}
			else 
				throw new Exception("Product tag not found");
		}
	}
}
