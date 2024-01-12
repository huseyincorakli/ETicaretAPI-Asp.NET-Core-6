using ETicaretAPI_V2.Application.Repositories.CommentRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text;
using SC = ETicaretAPI_V2.Application.DTOs.Comment;

namespace ETicaretAPI_V2.Application.Features.Commands.Comment.SummarizeComment
{
	public class SummarizeCommentCommandHandler : IRequestHandler<SummarizeCommentCommandRequest, SummarizeCommentCommandResponse>
	{
		readonly ICommentReadRepository _commentReadRepository;
		readonly IConfiguration _config;
		public SummarizeCommentCommandHandler(ICommentReadRepository commentReadRepository, IConfiguration config)
		{
			_commentReadRepository = commentReadRepository;
			_config = config;
		}

		public async Task<SummarizeCommentCommandResponse> Handle(SummarizeCommentCommandRequest request, CancellationToken cancellationToken)
		{
			var data = await _commentReadRepository.GetAll().Where(p => p.ProductId == Guid.Parse(request.ProductId)).ToListAsync();
			List<SC.SummarizeComment> comments = new();

			foreach (var item in data)
			{
				SC.SummarizeComment comment = new()
				{
					UserCommentTitle = item.UserCommentTitle,
					UserCommentContent = item.UserCommentContent,
					UserScore = item.UserScore
				};
				comments.Add(comment);
			}


			if (comments.Count<0)
			{
				return new()
				{
					ResponseData= "{\r\n \"summaryOfComments\":\"Ürünün yorum sayısı özetlenmek için uygun değil\",\r\n \"score_avarage\":\"0\"\r\n}"
				};
			}
			var requestData = new { comments = comments };
			string baseUrl = _config["SecretKeys:OPENAI_BASEURL"];

			string username = _config["SecretKeys:OPENAI_USERNAME"];
			string password = _config["SecretKeys:OPENAI_PASSWORD"];

			HttpClient client = new HttpClient();

			await Login(client, baseUrl, username, password);

			var datax = await MakePostRequest(client, baseUrl, requestData);

			if (datax!=null)
			{
				return new()
				{
					ResponseData = datax,
				};
			}
			else
			{
				return new() { ResponseData = null };
			}


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
