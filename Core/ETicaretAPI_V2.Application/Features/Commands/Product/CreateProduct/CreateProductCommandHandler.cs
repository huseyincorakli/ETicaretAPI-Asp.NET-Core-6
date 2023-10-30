using ETicaretAPI_V2.Application.Abstractions.Hubs;
using ETicaretAPI_V2.Application.Features.Commands.Product.CreateProduct;
using ETicaretAPI_V2.Application.Repositories.ProductRepositories;
using ETicaretAPI_V2.Application.Repositories.ProductTagRepositories;
using ETicaretAPI_V2.Domain.Entities;
using MediatR;

namespace ETicaretAPI.Application.Features.Commands.Product.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        readonly IProductWriteRepository _productWriteRepository;
        readonly IProductHubService _productHubService;
        readonly IProductTagReadRepository _productTagReadRepository;

		public CreateProductCommandHandler(IProductWriteRepository productWriteRepository, IProductHubService productHubService, IProductTagReadRepository productTagReadRepository)
		{
			_productWriteRepository = productWriteRepository;
			_productHubService = productHubService;
			_productTagReadRepository = productTagReadRepository;
		}

		public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            ICollection<ProductTag> productTags = new List<ProductTag>();

            for (int i = 0; i < request.ProductTagIds.Length; i++)
            {
                var productTag= await _productTagReadRepository.GetByIdAsync(request.ProductTagIds[i]);
                if (productTag!=null)
                {
					productTags.Add(productTag);
				}
            }

            await _productWriteRepository.AddAsync(new()
            {
                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock,
                Desciription = request.Description,
                ProductTags = productTags
            }) ;
            
            await _productWriteRepository.SaveAsync();
            await _productHubService.ProductAddedMessageAsync($"{request.Name} isminde ürün eklenmiştir.");
            return new();
        }
    }
}