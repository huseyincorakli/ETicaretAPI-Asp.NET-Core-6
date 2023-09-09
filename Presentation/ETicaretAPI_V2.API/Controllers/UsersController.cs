using ETicaretAPI_V2.Application.Abstraction.Services;
using ETicaretAPI_V2.Application.Features.Commands.AppUser.CreateUser;
using MediatR;
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
           
            CreateUserCommandResponse response= await _mediator.Send(createUserCommandRequest);
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> TestMail()
        {
            await _mailService.SendMessageAsync("huseyincorakli46@gmail.com", "Test", "<h1>Deneme<h1/>");
            return Ok();
        }

        
    }
}
