using ETicaretAPI_V2.Application.DTOs.Order;

namespace ETicaretAPI_V2.Application.Abstraction.Services
{
    public interface IOrderService
    {
        Task CreateOrderAsync(CreateOrder createOrder);
    }
}
