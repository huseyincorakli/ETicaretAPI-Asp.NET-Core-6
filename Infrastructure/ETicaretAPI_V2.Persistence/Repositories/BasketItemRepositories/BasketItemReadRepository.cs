using ETicaretAPI_V2.Application.Repositories.BasketItemRepositories;
using ETicaretAPI_V2.Domain.Entities;
using ETicaretAPI_V2.Persistence.Contexts;

namespace ETicaretAPI_V2.Persistence.Repositories.BasketItemRepositories
{
    public class BasketItemReadRepository : ReadRepository<BasketItem>, IBasketItemReadRepository
    {
        public BasketItemReadRepository(ETicaretAPI_V2DBContext context) : base(context)
        {
        }
    }
}
