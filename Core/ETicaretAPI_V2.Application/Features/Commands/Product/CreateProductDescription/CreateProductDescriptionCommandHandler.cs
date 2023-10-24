using ETicaretAPI_V2.Application.Abstraction.Services;
using ETicaretAPI_V2.Application.DTOs.ProductDesciription;
using MediatR;

namespace ETicaretAPI_V2.Application.Features.Commands.Product.CreateProductDescription
{
	public class CreateProductDescriptionCommandHandler : IRequestHandler<CreateProductDescriptionCommandRequest, CreateProductDescriptionCommandResponse>
	{
		readonly IGeneratorService _generatorService;

		public CreateProductDescriptionCommandHandler(IGeneratorService generatorService)
		{
			_generatorService = generatorService;
		}

		public async Task<CreateProductDescriptionCommandResponse> Handle(CreateProductDescriptionCommandRequest request, CancellationToken cancellationToken)
		{
			string description= await _generatorService.ProductDescriptionGenerator(request.Brand, request.Category, request.Description, request.Keywords, request.Name);

			return new()
			{
				Description = description
			};
		}
	}
}
