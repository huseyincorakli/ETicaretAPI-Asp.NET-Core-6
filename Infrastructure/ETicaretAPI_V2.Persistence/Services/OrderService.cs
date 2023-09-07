using ETicaretAPI_V2.Application.Abstraction.Services;
using ETicaretAPI_V2.Application.DTOs.Order;
using ETicaretAPI_V2.Application.Repositories.OrderRepositories;

namespace ETicaretAPI_V2.Persistence.Services
{
    public class OrderService : IOrderService
    {
        readonly IOrderReadRepository _orderReadRepository;
        readonly IOrderWriteRepository _orderWriteRepository;

        public OrderService(IOrderReadRepository orderReadRepository, IOrderWriteRepository orderWriteRepository)
        {
            _orderReadRepository = orderReadRepository;
            _orderWriteRepository = orderWriteRepository;
        }

        public async Task CreateOrderAsync(CreateOrder createOrder)
        {
            var orderCode = (new Random().NextDouble() * 10000).ToString();
            orderCode= orderCode.Substring(orderCode.IndexOf(",")+1,orderCode.Length-orderCode.IndexOf(",")-1);
           await _orderWriteRepository.AddAsync(new()
            {
                Address = createOrder.Address,
                Id = Guid.Parse(createOrder.BasketId),
                Description = createOrder.Description,
                OrderCode = orderCode
           });
            await _orderWriteRepository.SaveAsync();
        }
    }
}
