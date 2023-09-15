using ETicaretAPI_V2.Application.Repositories.MenuRepositories;
using ETicaretAPI_V2.Domain.Entities;
using ETicaretAPI_V2.Persistence.Contexts;

namespace ETicaretAPI_V2.Persistence.Repositories.MenuRepositories
{
    public class MenuWriteRepository : WriteRepository<Menu>, IMenuWriteRepository
    {
        public MenuWriteRepository(ETicaretAPI_V2DBContext context) : base(context)
        {
        }
    }
}
