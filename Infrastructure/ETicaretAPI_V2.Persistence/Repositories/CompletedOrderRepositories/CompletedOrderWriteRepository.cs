using ETicaretAPI_V2.Application.Repositories.CompletedOrderRepositories;
using ETicaretAPI_V2.Domain.Entities;
using ETicaretAPI_V2.Persistence.Contexts;

namespace ETicaretAPI_V2.Persistence.Repositories.CompletedOrderRepositories
{
    public class CompletedOrderWriteRepository : WriteRepository<CompletedOrder>, ICompletedOrderWriteRepository
    {
        public CompletedOrderWriteRepository(ETicaretAPI_V2DBContext context) : base(context)
        {
        }
    }
}
