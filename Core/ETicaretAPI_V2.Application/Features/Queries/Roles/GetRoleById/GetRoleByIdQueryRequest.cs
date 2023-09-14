using MediatR;

namespace ETicaretAPI_V2.Application.Features.Queries.Roles.GetRoleById
{
    public class GetRoleByIdQueryRequest:IRequest<GetRoleByIdQueryResponse>
    {
        public string Id { get; set; }
    }
}
