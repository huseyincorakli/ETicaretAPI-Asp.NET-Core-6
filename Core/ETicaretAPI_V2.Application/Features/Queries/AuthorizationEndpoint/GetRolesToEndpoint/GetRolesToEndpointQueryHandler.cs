using ETicaretAPI_V2.Application.Abstraction.Services;
using MediatR;

namespace ETicaretAPI_V2.Application.Features.Queries.AuthorizationEndpoint.GetRolesToEndpoint
{
    public class GetRolesToEndpointQueryHandler : IRequestHandler<GetRolesToEndpointQueryRequest, GetRolesToEndpointQueryResponse>
    {
        readonly IAuthorizationEndpointService _authorizationEndpointService;

        public GetRolesToEndpointQueryHandler(IAuthorizationEndpointService authorizationEndpointService)
        {
            _authorizationEndpointService = authorizationEndpointService;
        }

        public async Task<GetRolesToEndpointQueryResponse> Handle(GetRolesToEndpointQueryRequest request, CancellationToken cancellationToken)
        {
            var datas = await _authorizationEndpointService.GetRolesToEndpointAsync(request.Code,request.Menu);
            return new()
            {
                Roles = datas
            };
        }
    }
}
