using ETicaretAPI_V2.Application.Repositories.ProductTagRepositories;
using MediatR;

namespace ETicaretAPI_V2.Application.Features.Commands.ProductTag.CreateProductTag
{
	public class CreateProductTagCommandHandler : IRequestHandler<CreateProductTagCommandRequest, CreateProductTagCommandResponse>
	{
		private readonly IProductTagWriteRepository _productTagWriteRepository;

		public CreateProductTagCommandHandler(IProductTagWriteRepository productTagWriteRepository)
		{
			_productTagWriteRepository = productTagWriteRepository;
		}

		public async Task<CreateProductTagCommandResponse> Handle(CreateProductTagCommandRequest request, CancellationToken cancellationToken)
		{
			await _productTagWriteRepository.AddAsync(new()
			{
				CategoryId = Guid.Parse(request.CategoryId),
				TagName=request.ProductTagName,
				IsActive= request.IsActive,
				
			});
			await _productTagWriteRepository.SaveAsync();

			return new();
		}
	}
}
