using ETicaretAPI_V2.Application.Abstraction.Services;
using ETicaretAPI_V2.Application.DTOs.Order;
using ETicaretAPI_V2.Application.Repositories.CompletedOrderRepositories;
using ETicaretAPI_V2.Application.Repositories.OrderRepositories;
using ETicaretAPI_V2.Domain.Entities;
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
                   .ThenInclude(d => d.Product);
                   

            var data = query.Skip(page * size).Take(size);

            var data2 = from order in data
                        join completedOrder in _completedOrderReadRepository.Table
                        on order.Id equals completedOrder.OrderId into co
                        from _co in co.DefaultIfEmpty()
                        select new
                        {
                            Id=order.Id,
                            CreatedDate=order.CreateDate,
                            OrderCode= order.OrderCode,
                            Basket=order.Basket,
                            Completed=_co !=null ? true:false
                        };

            return new()
            {
                TotalOrderCount = await query.CountAsync(),
                Orders = await data2.Select(o => new
                {
                    Id = o.Id,
                    CreatedDate=o.CreatedDate,
                    OrderCode= o.OrderCode,
                    TotalPrice=o.Basket.BasketItems.Sum(bi=>bi.Quantity*bi.Product.Price),
                    UserName=o.Basket.User.UserName,
                    o.Completed
                }).ToListAsync()
            };
        }

        public async Task<SingleOrder> GetOrderByIdAsync(string id)
        {
            var data = _orderReadRepository.Table
                            .Include(o => o.Basket)
                            .ThenInclude(b => b.BasketItems)
                            .ThenInclude(bi => bi.Product);
                            
            var data2 = await (from order in data
                               join completedOrder in _completedOrderReadRepository.Table
                               on order.Id equals completedOrder.OrderId into co
                               from _co in co.DefaultIfEmpty()
                               select new
                               {
                                   Id = order.Id,
                                   CreateDate = order.CreateDate,
                                   OrderCode = order.OrderCode,
                                   Basket = order.Basket,
                                   Completed = _co != null ? true : false,
                                   Address=order.Address,
                                   Description=order.Description,
                               }).FirstOrDefaultAsync(o => o.Id == Guid.Parse(id)); ;
            return new()
            {

                Id = data2.Id.ToString(),
                BasketItems = data2.Basket.BasketItems.Select(bi => new
                {
                    bi.Product.Name,
                    bi.Product.Price,
                    bi.Quantity
                }),
                Address = data2.Address,
                CreatedDate = data2.CreateDate,
                Description = data2.Description,
                OrderCode = data2.OrderCode,
                Completed=data2.Completed
            };

        }
       
        public async Task CompleteOrderAsync(string id)
        {
            Order order = await _orderReadRepository.GetByIdAsync(id);
            if (order != null)
            {
                await _completedOrderWriteRepository.AddAsync(new CompletedOrder()
                {
                    OrderId = Guid.Parse(id),
                });
                await _completedOrderWriteRepository.SaveAsync();
            }
        }
    }

}
