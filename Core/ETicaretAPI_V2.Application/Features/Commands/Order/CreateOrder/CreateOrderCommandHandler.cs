using ETicaretAPI_V2.Application.Abstraction.Hubs;
using ETicaretAPI_V2.Application.Abstraction.Services;
using ETicaretAPI_V2.Application.Exceptions;
using ETicaretAPI_V2.Application.Repositories.DailySaleRepositories;
using ETicaretAPI_V2.Application.Repositories.ProductRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ETicaretAPI_V2.Application.Features.Commands.Order.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommandRequest, CreateOrderCommandResponse>
    {
        readonly IOrderService _orderService;
        readonly IBasketService _basketService;
        readonly IOrderHubService _orderHubService;
        readonly IProductWriteRepository _productWriteRepository;
        readonly IDailySaleWriteRepository _dailySaleWriteRepository;
        readonly IDailySaleReadRepository _dailySaleReadRepository;

		private async Task<DateTime> GetTurkeyCurrentTimeAsync()
		{
			using (HttpClient client = new HttpClient())
			{
				HttpResponseMessage response = await client.GetAsync("http://worldtimeapi.org/api/timezone/Europe/Istanbul");
				response.EnsureSuccessStatusCode();
				string responseData = await response.Content.ReadAsStringAsync();
				dynamic json = Newtonsoft.Json.JsonConvert.DeserializeObject(responseData);

				// 'datetime' alanını al ve DateTime tipine dönüştür
				DateTime turkeyTime = json.datetime;

				// Ensure that the DateTime is in UTC
				if (turkeyTime.Kind != DateTimeKind.Utc)
				{
					turkeyTime = turkeyTime.ToUniversalTime();
				}

				return turkeyTime;
			}
		}

		public CreateOrderCommandHandler(IOrderService orderService, IBasketService basketService, IOrderHubService orderHubService, IProductWriteRepository productWriteRepository, IDailySaleWriteRepository dailySaleWriteRepository, IDailySaleReadRepository dailySaleReadRepository)
		{
			_orderService = orderService;
			_basketService = basketService;
			_orderHubService = orderHubService;
			_productWriteRepository = productWriteRepository;
			_dailySaleWriteRepository = dailySaleWriteRepository;
			_dailySaleReadRepository = dailySaleReadRepository;
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
                    data.Product.QuantitySold = data.Product.QuantitySold + data.Quantity;
                    DateTime currentDate;
                    try
                    {
                        currentDate = await GetTurkeyCurrentTimeAsync();

					}
                    catch (Exception ex)
                    {

						throw new Exception("Turkey's current time couldn't be retrieved.", ex);
					}
					DateTime startOfDay = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 0, 0, 0, DateTimeKind.Utc);

					var dailySale = await _dailySaleReadRepository
						.GetAll()
						.FirstOrDefaultAsync(d => d.SalesTime.Date == currentDate.Date);
                    if (dailySale==null)
                    {
                       await _dailySaleWriteRepository.AddAsync(new()
                        {
                            Id = Guid.NewGuid(),
                            SalesTime = startOfDay,
                            SaleQuantity = data.Quantity,

                        });
                    }
                    else
                    {
                        int currentSaleQ= dailySale.SaleQuantity;
                        dailySale.SaleQuantity=currentSaleQ+data.Quantity;
                        await _dailySaleWriteRepository.SaveAsync();
                    }

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

