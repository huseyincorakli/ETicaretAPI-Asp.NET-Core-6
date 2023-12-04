using ETicaretAPI_V2.Application.Repositories.CommentRepositories;
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

		public WorkSpaceController(ICommentWriteRepository commentWriteRepository, ICommentReadRepository commentReadRepository)
		{
			_commentWriteRepository = commentWriteRepository;
			_commentReadRepository = commentReadRepository;
		}


		[HttpPost("[action]")]
		public async Task<IActionResult> AddComment(string productId,string content,string title,string nameSurname,float userScore)
		{
			await _commentWriteRepository.AddAsync(new()
			{
				Id =  Guid.NewGuid(),
				ProductId=Guid.Parse(productId),
				UserCommentContent=content,
				UserCommentTitle=title,
				UserNameSurname=nameSurname,
				UserScore=userScore
			});
			await _commentWriteRepository.SaveAsync();
			return Ok();
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> GetCommentsByProductId(string productId)
		{
			var data= await _commentReadRepository.GetAll().Where(p => p.ProductId == Guid.Parse(productId)).ToListAsync();
			return Ok(data);
		}
	}
}
