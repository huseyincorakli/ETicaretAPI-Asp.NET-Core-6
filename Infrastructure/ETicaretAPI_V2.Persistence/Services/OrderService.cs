using ETicaretAPI_V2.Application.Abstraction.Services;
using ETicaretAPI_V2.Application.DTOs.Order;
using ETicaretAPI_V2.Application.Repositories.CompletedOrderRepositories;
using ETicaretAPI_V2.Application.Repositories.OrderRepositories;
using Microsoft.EntityFrameworkCore;

namespace ETicaretAPI_V2.Persistence.Services
{
    public class OrderService : IOrderService
    {
        readonly IOrderReadRepository _orderReadRepository;
        readonly IOrderWriteRepository _orderWriteRepository;
        readonly ICompletedOrderReadRepository _completedOrderReadRepository;
        readonly ICompletedOrderWriteRepository _completedOrderWriteRepository;

        public OrderService(IOrderReadRepository orderReadRepository, IOrderWriteRepository orderWriteRepository, ICompletedOrderReadRepository completedOrderReadRepository, ICompletedOrderWriteRepository completedOrderWriteRepository)
        {
            _orderReadRepository = orderReadRepository;
            _orderWriteRepository = orderWriteRepository;
            _completedOrderReadRepository = completedOrderReadRepository;
            _completedOrderWriteRepository = completedOrderWriteRepository;
        }


        public async Task CreateOrderAsync(CreateOrder createOrder)
        {
            var orderCode = (new Random().NextDouble() * 10000).ToString();
            orderCode = orderCode.Substring(orderCode.IndexOf(",") + 1, orderCode.Length - orderCode.IndexOf(",") - 1);
            await _orderWriteRepository.AddAsync(new()
            {
                Address = createOrder.Address,
                Id = Guid.Parse(createOrder.BasketId),
                Description = createOrder.Description,
                OrderCode = orderCode
            });
            await _orderWriteRepository.SaveAsync();
        }

        public async Task<ListOrder> GetAllOrdersAsync(int page, int size)
        {
            var query = _orderReadRepository.Table.Include(x => x.Basket)
                   .ThenInclude(c => c.User)
                   .Include(a => a.Basket)
                   .ThenInclude(b => b.BasketItems)
                   .ThenInclude(d => d.Product)
                   .Select(o => new
                   {
                       Id = o.Id,
                       CreatedDate = o.CreateDate,
                       OrderCode = o.OrderCode,
                       TotalPrice = o.Basket.BasketItems.Sum(bi => bi.Product.Price * bi.Quantity),
                       UserName = o.Basket.User.UserName
                   });

            var data = query.Skip(page * size).Take(size);
            return new()
            {
                TotalOrderCount = await query.CountAsync(),
                Orders = data
            };
        }

        public async Task<SingleOrder> GetOrderByIdAsync(string id)
        {
            var data = await _orderReadRepository.Table
                            .Include(o => o.Basket)
                            .ThenInclude(b => b.BasketItems)
                            .ThenInclude(bi => bi.Product)
                            .FirstOrDefaultAsync(o => o.Id == Guid.Parse(id));

            return new()
            {

                Id = data.Id.ToString(),
                BasketItems = data.Basket.BasketItems.Select(bi => new
                {
                    bi.Product.Name,
                    bi.Product.Price,
                    bi.Quantity
                }),
                Address=data.Address,
                CreatedDate=data.CreateDate,
                Description= data.Description,
                OrderCode=data.OrderCode,

            };

        }

        public Task CompleteOrderAsync(string id)
        {
            throw new NotImplementedException();
        }
    }

}
