using ETicaretAPI_V2.Application.Consts;
using ETicaretAPI_V2.Application.CustomAttributes;
using ETicaretAPI_V2.Application.Enums;
using ETicaretAPI_V2.Application.Features.Commands.Comment.AddComment;
using ETicaretAPI_V2.Application.Features.Queries.Comment.GetCommentByProductId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI_V2.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CommentsController : ControllerBase
	{
		readonly IMediator _mediator;

		public CommentsController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost("[action]")]
		[Authorize(AuthenticationSchemes = "Admin")]
		[AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Comments, ActionType = ActionType.Writing, Definition = "Create Comment")]
		public async Task<IActionResult> AddComment([FromBody] AddCommentCommandRequest addCommentCommandRequest)
		{
			AddCommentCommandResponse response = await _mediator.Send(addCommentCommandRequest);


			return Ok(response);
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> GetCommentsByProductId([FromQuery] GetCommentByProductIdQueryRequest getCommentByProductIdQueryRequest)
		{
			var data = await _mediator.Send(getCommentByProductIdQueryRequest);
			return Ok(data);
		}
	}
}
