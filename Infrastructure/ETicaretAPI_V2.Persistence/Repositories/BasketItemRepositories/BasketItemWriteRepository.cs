using ETicaretAPI_V2.Application.Repositories.BasketItemRepositories;
using ETicaretAPI_V2.Domain.Entities;
using ETicaretAPI_V2.Persistence.Contexts;

namespace ETicaretAPI_V2.Persistence.Repositories.BasketItemRepositories
{
    public class BasketItemWriteRepository : WriteRepository<BasketItem>, IBasketItemWriteRepository
    {
        public BasketItemWriteRepository(ETicaretAPI_V2DBContext context) : base(context)
        {
        }
    }
}
