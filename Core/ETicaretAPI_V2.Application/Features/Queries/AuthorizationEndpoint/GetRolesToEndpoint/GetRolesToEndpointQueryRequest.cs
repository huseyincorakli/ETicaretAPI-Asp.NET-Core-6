using MediatR;

namespace ETicaretAPI_V2.Application.Features.Queries.AuthorizationEndpoint.GetRolesToEndpoint
{
    public class GetRolesToEndpointQueryRequest:IRequest<GetRolesToEndpointQueryResponse>
    {
        public string Code { get; set; }
        public string Menu { get; set; }
    }
}
