using Azure;
using ETicaretAPI_V2.Application.Abstraction.Services;
using ETicaretAPI_V2.Application.Consts;
using ETicaretAPI_V2.Application.CustomAttributes;
using ETicaretAPI_V2.Application.Enums;
using ETicaretAPI_V2.Application.Features.Commands.AppUser.AssignRoleToUser;
using ETicaretAPI_V2.Application.Features.Commands.AppUser.CreateUser;
using ETicaretAPI_V2.Application.Features.Commands.AppUser.UpdatePassword;
using ETicaretAPI_V2.Application.Features.Commands.AppUser.UpdateProfile;
using ETicaretAPI_V2.Application.Features.Queries.AppUser.GetAllUsers;
using ETicaretAPI_V2.Application.Features.Queries.AppUser.GetRolesToUser;
using ETicaretAPI_V2.Application.Features.Queries.AppUser.GetUserById;
using ETicaretAPI_V2.Infrastructure.Services.Mail;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI_V2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IMediator _mediator;
        readonly IMailService _mailService;


        public UsersController(IMediator mediator, IMailService mailService)
        {
            _mediator = mediator;
            _mailService = mailService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommandRequest createUserCommandRequest)
        {

            CreateUserCommandResponse response = await _mediator.Send(createUserCommandRequest);
            return Ok(response);
        }
        

        [HttpPost("update-password")]
		
		public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordCommandRequest updatePasswordCommandRequest)
        {
            UpdatePasswordCommandResponse response = await _mediator.Send(updatePasswordCommandRequest);
            return Ok(response);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(ActionType = Application.Enums.ActionType.Reading, Definition = "Get All Users", Menu = "Users")]
        public async Task<IActionResult> GetAllUsers([FromQuery]GetAllUsersQueryRequest getAllUsersQueryRequest)
        {
            GetAllUsersQueryResponse response = await _mediator.Send(getAllUsersQueryRequest);
            return Ok(response);
        }

        [HttpGet("get-roles-to-user/{UserId}")]
        
        public async Task<IActionResult> GetRolesToUser([FromRoute] GetRolesToUserQueryRequest  getRolesToUserQueryRequest)
        {
          GetRolesToUserQueryResponse response =   await _mediator.Send(getRolesToUserQueryRequest);
            return Ok(response);
        }

        [HttpPost("assign-role-to-user")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(ActionType = Application.Enums.ActionType.Writing, Definition = "Assign Role To User", Menu = "Users")]
        public async Task<IActionResult> AssignRoleToUser([FromBody]AssignRoleToUserCommandRequest roleToUserCommandRequest)
        {
            AssignRoleToUserCommandResponse response= await _mediator.Send(roleToUserCommandRequest);
            return Ok(response);
        }

        [HttpPut("[action]")]
		[Authorize(AuthenticationSchemes = "Admin")]
		[AuthorizeDefinition(ActionType = Application.Enums.ActionType.Updating, Definition = "Update Profile", Menu = "Users")]
		public async Task<IActionResult> UpdateProfile([FromBody]UpdateProfileCommandRequest updateProfileCommandRequest)
        {
            var response = await _mediator.Send(updateProfileCommandRequest);
            return Ok(response);
        }

        [HttpGet("[action]")]

        public async Task<IActionResult> GetUserById([FromQuery] GetUserByIdQueryRequest getUserByIdQueryRequest)
        {
            var response = await _mediator.Send(getUserByIdQueryRequest);
            return Ok(response);
        }
    }
}


//[HttpGet]
//public async Task<IActionResult> TestMail()
//{
//    await _mailService.SendMailAsync("huseyincorakli46@gmail.com", "Test", "<h1>Deneme<h1/>");
//    return Ok();
//}