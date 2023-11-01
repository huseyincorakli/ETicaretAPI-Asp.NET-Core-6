using ETicaretAPI_V2.Application.Abstraction.Hubs;
using ETicaretAPI_V2.Application.Abstraction.Services;
using ETicaretAPI_V2.Application.Exceptions;
using ETicaretAPI_V2.Application.Repositories.ProductRepositories;
using MediatR;

namespace ETicaretAPI_V2.Application.Features.Commands.Order.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommandRequest, CreateOrderCommandResponse>
    {
        readonly IOrderService _orderService;
        readonly IBasketService _basketService;
        readonly IOrderHubService _orderHubService;
        readonly IProductWriteRepository _productWriteRepository;
		public CreateOrderCommandHandler(IOrderService orderService, IBasketService basketService, IOrderHubService orderHubService, IProductWriteRepository productWriteRepository)
		{
			_orderService = orderService;
			_basketService = basketService;
			_orderHubService = orderHubService;
			_productWriteRepository = productWriteRepository;
		}

		public async Task<CreateOrderCommandResponse> Handle(CreateOrderCommandRequest request, CancellationToken cancellationToken)
        {
           
           var datas =  await _basketService.GetBasketItemsAsync();
            foreach (var data  in datas)
            {
                if (data.Quantity>data.Product.Stock)
                {
                    throw new NoStockException($"{data.Product.Name} is not enough there is {data.Product.Stock}");
                }
                else if (data.Quantity <= 0)
                {
                    throw new NoStockException("Quantity cannot be 0 and below");
                }
                else
                {
                    data.Product.Stock = data.Product.Stock - data.Quantity;
                    await _productWriteRepository.SaveAsync();
                }
            }
            await _orderService.CreateOrderAsync(new()
            {
                Address = request.Address,
                Description = request.Description,
                BasketId = (_basketService.GetUserActiveBasket?.Id).ToString()
            });
            var orderAddedMessage = request.Description + " " + request.Description +" "+" == Yeni Sipariş Geldi!";
            await _orderHubService.OrderAddedMessageAsync(orderAddedMessage);
            return new();
        }
    }
}
