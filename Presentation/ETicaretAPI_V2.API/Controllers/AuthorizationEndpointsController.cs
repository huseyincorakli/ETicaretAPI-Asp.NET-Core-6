using ETicaretAPI_V2.Application.Features.Commands.AuthorizationEndpoint.AssignRoleEndpoint;
using ETicaretAPI_V2.Application.Features.Queries.AuthorizationEndpoint.GetRolesToEndpoint;
using ETicaretAPI_V2.Application.Features.Queries.Roles.GetRoles;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI_V2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationEndpointsController : ControllerBase
    {
        readonly IMediator _mediator;

        public AuthorizationEndpointsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetRolesToEndpoint(GetRolesToEndpointQueryRequest getRolesToEndpointQueryRequest)
        {
            var data = await _mediator.Send(getRolesToEndpointQueryRequest);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRoleEndpoint(AssignRoleEndpointCommandRequest assignRoleEndpointCommandRequest)
        {
            assignRoleEndpointCommandRequest.Type = typeof(Program);
            AssignRoleEndpointCommandResponse response= await _mediator.Send(assignRoleEndpointCommandRequest);
            return Ok(response);
        }
    }
}
