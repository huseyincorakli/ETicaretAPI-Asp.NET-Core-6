using Azure.Messaging;
using ETicaretAPI_V2.Application.Abstraction.Services;
using ETicaretAPI_V2.Application.DTOs.Comment;
using ETicaretAPI_V2.Application.Features.Commands.Campaign.CreateCampaign;
using ETicaretAPI_V2.Application.Features.Commands.Campaign.DeleteCampaign;
using ETicaretAPI_V2.Application.Features.Commands.Campaign.UpdateShowcase;
using ETicaretAPI_V2.Application.Features.Commands.Comment.AddComment;
using ETicaretAPI_V2.Application.Features.Queries.Campaign.GetActiveCampaign;
using ETicaretAPI_V2.Application.Features.Queries.Campaign.GetAllCampaign;
using ETicaretAPI_V2.Application.Features.Queries.Campaign.GetCampaignById;
using ETicaretAPI_V2.Application.Features.Queries.Comment.GetCommentByProductId;
using ETicaretAPI_V2.Application.Features.Queries.Comment.UserHasComment;
using ETicaretAPI_V2.Application.Repositories.CampaignUsageRepositories;
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
		readonly ICampaignUsageWriteRepository _campaignUsageWriteRepository;
		readonly ICampaignUsageReadRepository _campaignUsageReadRepository;

		readonly IMediator _mediator;
		private readonly HttpClient _httpClient;
		readonly IConfiguration configuration;
		readonly ICommentService _commentService;
		readonly ICampaignService _campaignService;
		public WorkSpaceController(ICommentWriteRepository commentWriteRepository, ICommentReadRepository commentReadRepository, IMediator mediator, IConfiguration configuration, HttpClient httpClient, IHttpClientFactory httpClientFactory, ICommentService commentService, ICampaignService campaignService, ICampaignUsageReadRepository campaignUsageReadRepository, ICampaignUsageWriteRepository campaignUsageWriteRepository)
		{
			_commentWriteRepository = commentWriteRepository;
			_commentReadRepository = commentReadRepository;
			_mediator = mediator;
			this.configuration = configuration;
			_httpClient = httpClientFactory.CreateClient();
			_commentService = commentService;
			_campaignService = campaignService;
			_campaignUsageReadRepository = campaignUsageReadRepository;
			_campaignUsageWriteRepository = campaignUsageWriteRepository;
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
		

		[HttpPost("[action]")]
		public async Task<IActionResult> SetCampaignUsage([FromQuery] string userId,string campaignId )
		{
			var data = await _campaignUsageReadRepository.GetSingleAsync(s => s.UserId == userId && s.CampaignId == Guid.Parse(campaignId));
			if (data==null)
			{
				await _campaignUsageWriteRepository.AddAsync(new()
				{
					CampaignId = Guid.Parse(campaignId),
					UserId = userId,
					UsageTime = DateTime.UtcNow,
					Id = Guid.NewGuid()
				});
				 await _campaignUsageWriteRepository.SaveAsync();
			}
			else
			{
				throw new Exception("KULLANILMIŞ");
			}
			return Ok();
		}
		[HttpGet("[action]")]
		public async Task<IActionResult> GetCampaignUsage([FromQuery] string userId, string campaignId)
		{
			var data = await _campaignUsageReadRepository.GetSingleAsync(s=>s.UserId==userId && s.CampaignId==Guid.Parse(campaignId));

			if (data == null)
			{
				return Ok(false);
			}
			else
				return Ok(true);
		}
		[HttpGet("[action]")]
		public async Task<IActionResult> SummarizeCommentAPI([FromQuery] string productId)
		{
			var data = await _commentReadRepository.GetAll().Where(p => p.ProductId == Guid.Parse(productId)).ToListAsync();
			List<SummarizeComment> comments = new();

			foreach (var item in data)
			{
				SummarizeComment comment = new()
				{
					UserCommentTitle = item.UserCommentTitle,
					UserCommentContent = item.UserCommentContent,
					UserScore = item.UserScore
				};
				comments.Add(comment);
			}

			var requestData = new { comments = comments };
			string baseUrl = ",,";

			string username = ",,";
			string password = ",,";

			HttpClient client = new HttpClient();

			// Step 1: Login
			await Login(client, baseUrl, username, password);

			// Step 2: Make POST request
			var datax = await MakePostRequest(client, baseUrl, requestData);

			return Ok(datax);
		}

		static async Task Login(HttpClient client, string baseUrl, string username, string password)
		{
			HttpResponseMessage response = await client.GetAsync($"{baseUrl}/login?username={username}&password={password}");

			if (response.IsSuccessStatusCode)
			{
				Console.WriteLine("Login successful");
			}
			else
			{
				Console.WriteLine($"Login failed. Status code: {response.StatusCode}");
			}
		}

		static async Task<string> MakePostRequest(HttpClient client, string baseUrl, object requestData)
		{
			var requestBody = requestData;

			string requestBodyJson = Newtonsoft.Json.JsonConvert.SerializeObject(requestBody);

			var content = new StringContent(requestBodyJson, Encoding.UTF8, "application/json");

			HttpResponseMessage response = await client.PostAsync($"{baseUrl}/", content);

			if (response.IsSuccessStatusCode)
			{
				string responseContent = await response.Content.ReadAsStringAsync();
				return responseContent;
			}
			else
			{
				return null;
			}
		}
	}
}
