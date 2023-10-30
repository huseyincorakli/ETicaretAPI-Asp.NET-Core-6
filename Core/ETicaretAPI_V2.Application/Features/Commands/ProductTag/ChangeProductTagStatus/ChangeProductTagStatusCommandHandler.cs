using ETicaretAPI_V2.Application.Repositories.ProductTagRepositories;
using MediatR;

namespace ETicaretAPI_V2.Application.Features.Commands.ProductTag.ChangeProductTagStatus
{
	public class ChangeProductTagStatusCommandHandler : IRequestHandler<ChangeProductTagStatusCommandRequest, ChangeProductTagStatusCommandResponse>
	{
		private readonly IProductTagReadRepository _productTagReadRepository;
		private readonly IProductTagWriteRepository _productTagWriteRepository;

		public ChangeProductTagStatusCommandHandler(IProductTagWriteRepository productTagWriteRepository, IProductTagReadRepository productTagReadRepository)
		{
			_productTagWriteRepository = productTagWriteRepository;
			_productTagReadRepository = productTagReadRepository;
		}

		public async Task<ChangeProductTagStatusCommandResponse> Handle(ChangeProductTagStatusCommandRequest request, CancellationToken cancellationToken)
		{
			var productTag=  await _productTagReadRepository.GetByIdAsync(request.ProductTagId);
			if (productTag != null)
			{
				productTag.IsActive = request.IsActive;
				productTag.UpdatedDate= DateTime.UtcNow;
				await _productTagWriteRepository.SaveAsync();

				return new();
			}
			else
			{
				throw new Exception("Product tag not found!");
			}
		}
	}
}
