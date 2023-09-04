using ETicaretAPI_V2.Application.Repositories.BasketRepositories;
using ETicaretAPI_V2.Domain.Entities;
using ETicaretAPI_V2.Persistence.Contexts;

namespace ETicaretAPI_V2.Persistence.Repositories.BasketRepositories
{
    public class BasketWriteRepository : WriteRepository<Basket>, IBasketWriteRepository
    {
        public BasketWriteRepository(ETicaretAPI_V2DBContext context) : base(context)
        {
        }
    }
}
