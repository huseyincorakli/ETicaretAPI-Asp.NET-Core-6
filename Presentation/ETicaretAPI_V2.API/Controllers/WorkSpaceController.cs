﻿using ETicaretAPI_V2.Application.Features.Commands.Comment.AddComment;
using ETicaretAPI_V2.Application.Features.Queries.Comment.GetCommentByProductId;
using ETicaretAPI_V2.Application.Repositories.CommentRepositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ETicaretAPI_V2.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class WorkSpaceController : ControllerBase
	{
		readonly ICommentWriteRepository _commentWriteRepository;
		readonly ICommentReadRepository _commentReadRepository;
		readonly IMediator _mediator;

		public WorkSpaceController(ICommentWriteRepository commentWriteRepository, ICommentReadRepository commentReadRepository, IMediator mediator)
		{
			_commentWriteRepository = commentWriteRepository;
			_commentReadRepository = commentReadRepository;
			_mediator = mediator;
		}


		[HttpPost("[action]")]
		public async Task<IActionResult> AddComment([FromBody] AddCommentCommandRequest addCommentCommandRequest)
		{
			AddCommentCommandResponse response =  await _mediator.Send(addCommentCommandRequest);

			
			return Ok(response);
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> GetCommentsByProductId([FromQuery]GetCommentByProductIdQueryRequest getCommentByProductIdQueryRequest)
		{
			var data = await _mediator.Send(getCommentByProductIdQueryRequest);
			return Ok(data);
		}
	}
}
