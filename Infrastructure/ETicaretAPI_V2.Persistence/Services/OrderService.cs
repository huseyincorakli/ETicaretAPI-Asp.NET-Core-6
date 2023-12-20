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


		public async Task<ListOrder> GetOrderByUserId(int size,string userId)
		{
			var query = _orderReadRepository.Table.Include(x => x.Basket)
				   .ThenInclude(c => c.User)
				   .Include(a => a.Basket)
				   .ThenInclude(b => b.BasketItems)
				   .ThenInclude(d => d.Product);

			var data = await query.Where(a=>a.Basket.UserId==userId).ToListAsync();

			var data2 = from order in data
						join completedOrder in _completedOrderReadRepository.Table
						on order.Id equals completedOrder.OrderId into co
						from _co in co.DefaultIfEmpty()
						select new
						{
							Id = order.Id,
							CreatedDate = order.CreateDate,
							OrderCode = order.OrderCode,
							Basket = order.Basket,
							Completed = _co != null ? true : false,
							
						};
			
			var filteredData = data2;
			var sortedData = filteredData.OrderByDescending(a => a.CreatedDate).ToList().Take(size);
			
			return new ListOrder
			{
				TotalOrderCount = filteredData.Count(),
				Orders = sortedData.Select(o => new
				{
					Id = o.Id,
					CreatedDate = o.CreatedDate,
					OrderCode = o.OrderCode,
					TotalPrice = o.Basket.BasketItems.Sum(bi => bi.Quantity * bi.Product.Price),
					UserName = o.Basket.User.NameSurname,
					o.Completed,
					
				}).OrderByDescending(o => o.CreatedDate).ToList()
			};
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

        public async Task<ListOrder> GetUnCompletedOrders(int size)
        {
			var query = _orderReadRepository.Table.Include(x => x.Basket)
				   .ThenInclude(c => c.User)
				   .Include(a => a.Basket)
				   .ThenInclude(b => b.BasketItems)
				   .ThenInclude(d => d.Product);

			var data = await query.ToListAsync();
			var data2 = from order in data
						join completedOrder in _completedOrderReadRepository.Table
						on order.Id equals completedOrder.OrderId into co
						from _co in co.DefaultIfEmpty()
						select new
						{
							Id = order.Id,
							CreatedDate = order.CreateDate,
							OrderCode = order.OrderCode,
							Basket = order.Basket,
							Completed = _co != null ? true : false
						};

			var filteredData = data2.Where(a => a.Completed == false);
            var sortedData= filteredData.OrderBy(a=>a.CreatedDate).ToList().Take(size);
			return new ListOrder
			{
				TotalOrderCount = filteredData.Count(),
				Orders = sortedData.Select(o => new
				{
					Id = o.Id,
					CreatedDate = o.CreatedDate,
					OrderCode = o.OrderCode,
					TotalPrice = o.Basket.BasketItems.Sum(bi => bi.Quantity * bi.Product.Price),
					UserName = o.Basket.User.NameSurname,
					o.Completed
				}).OrderBy(o => o.CreatedDate).ToList()
			};
		}

		public async Task<ListOrder> GetAllOrdersAsync(int page, int size, bool isCompleted, string orderCode)
		{
			var query = _orderReadRepository.Table.Include(x => x.Basket)
				   .ThenInclude(c => c.User)
				   .Include(a => a.Basket)
				   .ThenInclude(b => b.BasketItems)
				   .ThenInclude(d => d.Product);

			var data = await query.ToListAsync();

			var data2 = from order in data
						join completedOrder in _completedOrderReadRepository.Table
						on order.Id equals completedOrder.OrderId into co
						from _co in co.DefaultIfEmpty()
						select new
						{
							Id = order.Id,
							CreatedDate = order.CreateDate,
							OrderCode = order.OrderCode,
							Basket = order.Basket,
							Completed = _co != null ? true : false
						};

			var filteredData = data2.Where(a => a.Completed == isCompleted);
            if (orderCode!="")
            {
                filteredData = filteredData.Where(a => a.OrderCode == orderCode);
            }
			var paginatedData = filteredData.Skip(page * size).Take(size);

			return new ListOrder
			{
				TotalOrderCount = filteredData.Count(), 
				Orders = paginatedData.Select(o => new
				{
					Id = o.Id,
					CreatedDate = o.CreatedDate,
					OrderCode = o.OrderCode,
					TotalPrice = o.Basket.BasketItems.Sum(bi => bi.Quantity * bi.Product.Price),
					UserName = o.Basket.User.UserName,
					o.Completed
				}).OrderBy(o => o.CreatedDate).ToList()
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
                                   Address = order.Address,
                                   Description = order.Description,
                               }).FirstOrDefaultAsync(o => o.Id == Guid.Parse(id)); ;
            return new()
            {

                Id = data2.Id.ToString(),
                BasketItems = data2.Basket.BasketItems.Select(bi => new
                {
                    bi.ProductId,
                    bi.Product.Name,
                    bi.Product.Price,
                    bi.Quantity
                }),
				
                Address = data2.Address,
                CreatedDate = data2.CreateDate,
                Description = data2.Description,
                OrderCode = data2.OrderCode,
                Completed = data2.Completed
            };

        }

        public async Task<(bool, CompletedOrderDTO)> CompleteOrderAsync(string id,string trackCode,string companyId)
        {
            Order? order = await _orderReadRepository.Table
                .Include(o => o.Basket)
                .ThenInclude(b => b.User)
                .FirstOrDefaultAsync(o => o.Id == Guid.Parse(id));
            if (order != null)
            {
                await _completedOrderWriteRepository.AddAsync(new CompletedOrder()
                {
                    OrderId = Guid.Parse(id),
					CargoTrackingCode = trackCode,
					ShippingCompanyId = Guid.Parse(companyId),
				});
                return (await _completedOrderWriteRepository.SaveAsync() > 0, new()
                {
                    OrderCode = order.OrderCode,
                    OrderDate = order.CreateDate,
                    Username = order.Basket.User.UserName,
                    UserSurname = order.Basket.User.NameSurname,
                    Email = order.Basket.User.Email,
					
                });
            }
            return (false, null);

        }
    }

}
