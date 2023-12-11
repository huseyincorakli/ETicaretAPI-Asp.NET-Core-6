using Azure.Messaging;
using ETicaretAPI_V2.Application.Abstraction.Services;
using ETicaretAPI_V2.Application.Features.Commands.Campaign.CreateCampaign;
using ETicaretAPI_V2.Application.Features.Commands.Campaign.DeleteCampaign;
using ETicaretAPI_V2.Application.Features.Commands.Campaign.UpdateShowcase;
using ETicaretAPI_V2.Application.Features.Commands.Comment.AddComment;
using ETicaretAPI_V2.Application.Features.Queries.Campaign.GetActiveCampaign;
using ETicaretAPI_V2.Application.Features.Queries.Campaign.GetAllCampaign;
using ETicaretAPI_V2.Application.Features.Queries.Campaign.GetCampaignById;
using ETicaretAPI_V2.Application.Features.Queries.Comment.GetCommentByProductId;
using ETicaretAPI_V2.Application.Features.Queries.Comment.UserHasComment;
using ETicaretAPI_V2.Application.Repositories.CommentRepositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace ETicaretAPI_V2.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class WorkSpaceController : ControllerBase
	{
		readonly ICommentWriteRepository _commentWriteRepository;
		readonly ICommentReadRepository _commentReadRepository;
		readonly IMediator _mediator;
		private readonly HttpClient _httpClient;
		readonly IConfiguration configuration;
		readonly ICommentService _commentService;
		readonly ICampaignService _campaignService;
		public WorkSpaceController(ICommentWriteRepository commentWriteRepository, ICommentReadRepository commentReadRepository, IMediator mediator, IConfiguration configuration, HttpClient httpClient, IHttpClientFactory httpClientFactory, ICommentService commentService, ICampaignService campaignService)
		{
			_commentWriteRepository = commentWriteRepository;
			_commentReadRepository = commentReadRepository;
			_mediator = mediator;
			this.configuration = configuration;
			_httpClient = httpClientFactory.CreateClient();
			_commentService = commentService;
			_campaignService = campaignService;
		}


		[HttpPost("[action]")]
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

		[HttpGet("[action]")]
		public async Task<IActionResult> GetCommentsByUserId([FromQuery] UserHasCommentQueryRequest userHasCommentQueryRequest)
		{
			UserHasCommentQueryResponse response = await _mediator.Send(userHasCommentQueryRequest);


			return Ok(response);
		}
		
	}
}
