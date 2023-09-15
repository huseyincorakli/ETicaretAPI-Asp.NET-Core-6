using ETicaretAPI_V2.Application.Repositories.EndpointRepositories;
using ETicaretAPI_V2.Domain.Entities;
using ETicaretAPI_V2.Persistence.Contexts;

namespace ETicaretAPI_V2.Persistence.Repositories.EnpointRepositories
{
    public class EndpointWriteRepository : WriteRepository<Endpoint>, IEndpointWriteRepository
    {
        public EndpointWriteRepository(ETicaretAPI_V2DBContext context) : base(context)
        {
        }
    }
}
