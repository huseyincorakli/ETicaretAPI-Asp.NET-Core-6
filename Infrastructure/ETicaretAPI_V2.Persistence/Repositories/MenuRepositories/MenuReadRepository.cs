using ETicaretAPI_V2.Application.Repositories.MenuRepositories;
using ETicaretAPI_V2.Domain.Entities;
using ETicaretAPI_V2.Persistence.Contexts;

namespace ETicaretAPI_V2.Persistence.Repositories.MenuRepositories
{
    public class MenuReadRepository : ReadRepository<Menu>, IMenuReadRepository
    {
        public MenuReadRepository(ETicaretAPI_V2DBContext context) : base(context)
        {
        }
    }
}
