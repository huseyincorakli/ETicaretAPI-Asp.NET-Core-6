using ETicaretAPI_V2.Application.Abstraction.Services;
using ETicaretAPI_V2.Application.CustomAttributes;
using ETicaretAPI_V2.Application.Features.Commands.AppUser.CreateUser;
using ETicaretAPI_V2.Application.Features.Commands.AppUser.UpdatePassword;
using ETicaretAPI_V2.Application.Features.Queries.AppUser.GetAllUsers;
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
    }
}


//[HttpGet]
//public async Task<IActionResult> TestMail()
//{
//    await _mailService.SendMailAsync("huseyincorakli46@gmail.com", "Test", "<h1>Deneme<h1/>");
//    return Ok();
//}