using ETicaretAPI_V2.Application.DTOs.Order;
using ETicaretAPI_V2.Domain.Entities;

namespace ETicaretAPI_V2.Application.Abstraction.Services
{
    public interface IOrderService
    {
        Task<SingleOrder> GetOrderByOrderCode(string orderCode);
		Task<ListOrder> GetOrderByUserId(int size, string userId);
		Task CreateOrderAsync(CreateOrder createOrder);
        Task<ListOrder> GetAllOrdersAsync(int page,int size,bool isCompleted,string orderCode);
        Task<SingleOrder> GetOrderByIdAsync(string id);
        Task<(bool, CompletedOrderDTO)> CompleteOrderAsync(string id, string trackCode, string companyId);
        Task<ListOrder> GetUnCompletedOrders(int size);

	}
}
