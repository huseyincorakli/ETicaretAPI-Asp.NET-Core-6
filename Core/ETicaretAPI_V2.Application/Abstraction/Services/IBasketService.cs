using ETicaretAPI_V2.Application.ViewModels.Baskets;
using ETicaretAPI_V2.Domain.Entities;

namespace ETicaretAPI_V2.Application.Abstraction.Services
{
    public  interface IBasketService
    {
        public Task<List<BasketItem>> GetBasketItemsAsync();
        public Task AddItemToBasketAsync(VM_Create_BasketItem basketItem);
        public Task UpdateQuantityAsync(VM_Update_BasketItem basketItem);
        public Task RemoveBasketItemAsync(string basketItemId);
        public Basket? GetUserActiveBasket { get; }
    }
}
