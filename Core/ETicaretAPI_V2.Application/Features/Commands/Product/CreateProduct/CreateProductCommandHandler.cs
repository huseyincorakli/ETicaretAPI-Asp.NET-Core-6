using ETicaretAPI_V2.Application.Abstractions.Hubs;
using ETicaretAPI_V2.Application.Features.Commands.Product.CreateProduct;
using ETicaretAPI_V2.Application.Repositories.ProductRepositories;
using MediatR;

namespace ETicaretAPI.Application.Features.Commands.Product.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        readonly IProductWriteRepository _productWriteRepository;
        readonly IProductHubService _productHubService;

        public CreateProductCommandHandler(IProductWriteRepository productWriteRepository, IProductHubService productHubService)
        {
            _productWriteRepository = productWriteRepository;
            _productHubService = productHubService;
        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            await _productWriteRepository.AddAsync(new()
            {
                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock,
                Desciription = request.Description
            }) ;
            
            await _productWriteRepository.SaveAsync();
            await _productHubService.ProductAddedMessageAsync($"{request.Name} isminde ürün eklenmiştir.");
            return new();
        }
    }
}