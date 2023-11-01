using ETicaretAPI_V2.Application.Abstraction.Services;
using ETicaretAPI_V2.Application.Exceptions;
using ETicaretAPI_V2.Application.Repositories.ProductRepositories;
using MediatR;
using pt = ETicaretAPI_V2.Domain.Entities;
namespace ETicaretAPI_V2.Application.Features.Commands.Basket.AddItemToBasket
{
    public class AddItemToBasketCommandHandler : IRequestHandler<AddItemToBasketCommandRequest, AddItemToBasketCommandResponse>
    {
        readonly IBasketService _basketService;
        readonly IProductWriteRepository _productWriteRepository;
        readonly IProductReadRepository _productReadRepository;

		public AddItemToBasketCommandHandler(IBasketService basketService, IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
		{
			_basketService = basketService;
			_productWriteRepository = productWriteRepository;
			_productReadRepository = productReadRepository;
		}

		public async Task<AddItemToBasketCommandResponse> Handle(AddItemToBasketCommandRequest request, CancellationToken cancellationToken)
        {
            pt.Product product = await _productReadRepository.GetByIdAsync(request.ProductId);
            if (product.Stock < request.Quantity)
            {
                throw new NoStockException();
            }
            else
                await _basketService.AddItemToBasketAsync(new()
                {
                    ProductId = request.ProductId,
                    Quantity = request.Quantity,
                });

            return new();
        }
    }
}
