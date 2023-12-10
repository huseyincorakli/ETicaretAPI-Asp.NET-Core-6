using ETicaretAPI_V2.Application.Abstraction.Services;
using ETicaretAPI_V2.Application.DTOs.Comment;
using ETicaretAPI_V2.Application.Repositories.CommentRepositories;
using ETicaretAPI_V2.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

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

		public async Task<(List<Comment>,int TotalCount , float averageRating)> GetCommentByProductIdAsync(string productId,int Size,int Page)
		{
			var query = _commentReadRepository.GetAll();
			
			var total = await _commentReadRepository.GetAll().ToListAsync();

			var query2 =  query.Where(p => p.ProductId == Guid.Parse(productId));
			List<Comment> data = await query2.Skip(Size*Page).Take(Size).ToListAsync();
			int tCount = (await query2.ToListAsync()).Count;
			float totalScore = 0;
			foreach (var item in await query2.ToListAsync())
			{
				totalScore += item.UserScore;
			}
			float avarageScore = totalScore / tCount;
			return (data, tCount, avarageScore);
		}

		public async Task<bool> UserHasComment(string userId,string productId)
		{
			var data =  _commentReadRepository.GetAll(false).Where(p => p.UserId == userId);
			if (data==null)
			{
				return false;
			}
			else
			{
				var isHas = await data.Where(x => x.ProductId == Guid.Parse(productId)).ToListAsync();
				if (isHas.Count==0)
				{
					return false;
				}
				else
					return true;
			}
		}
		

		public async Task<object> UserComment(string userId,string productId)
		{
			var data = await _commentReadRepository.GetAll(false).Where(p => p.UserId == userId && p.ProductId==Guid.Parse(productId)).FirstAsync();
			return data;
		}
	}
}
