using ETicaretAPI_V2.Application.Repositories.BasketRepositories;
using ETicaretAPI_V2.Domain.Entities;
using ETicaretAPI_V2.Persistence.Contexts;

namespace ETicaretAPI_V2.Persistence.Repositories.BasketRepositories
{
    public class BasketReadRepository : ReadRepository<Basket>, IBasketReadRepository
    {
        public BasketReadRepository(ETicaretAPI_V2DBContext context) : base(context)
        {
        }
    }
}
