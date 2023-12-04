using ETicaretAPI_V2.Application.Abstraction.Services;
using ETicaretAPI_V2.Application.DTOs.Comment;
using ETicaretAPI_V2.Application.Repositories.CommentRepositories;
using Microsoft.EntityFrameworkCore;

namespace ETicaretAPI_V2.Persistence.Services
{
	public class CommentService : ICommentService
	{
		readonly ICommentReadRepository _commentReadRepository;
		readonly ICommentWriteRepository _commentWriteRepository;

		public CommentService(ICommentReadRepository commentReadRepository, ICommentWriteRepository commentWriteRepository)
		{
			_commentReadRepository = commentReadRepository;
			_commentWriteRepository = commentWriteRepository;
		}

		public async Task<bool> CreateCommentAsync(CreateComment createComment)
		{
			if (createComment != null)
			{
				await _commentWriteRepository.AddAsync(new()
				{
					Id = Guid.NewGuid(),
					ProductId = Guid.Parse(createComment.ProductId),
					UserCommentContent = createComment.CommentContent,
					UserCommentTitle = createComment.CommentTitle,
					UserNameSurname = createComment.NameSurname,
					UserScore = createComment.Score,
					UserId = createComment.UserId
				});
				var data = await _commentWriteRepository.SaveAsync();
				if (data == 1)
					return true;
				else
					return false;
			}
			else
				throw new Exception("CreateComment cannot be null");
			
			
		}

		public async Task<object> GetCommentByProductIdAsync(string productId)
		{
			object data = await _commentReadRepository.GetAll().Where(p => p.ProductId == Guid.Parse(productId)).ToListAsync();
			return data;
		}

		public Task<object> GetCommentByUserIdAsync(string userId)
		{
			throw new NotImplementedException();
		}
	}
}
