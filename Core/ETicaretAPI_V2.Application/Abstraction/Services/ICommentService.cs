using ETicaretAPI_V2.Application.DTOs.Comment;
using ETicaretAPI_V2.Domain.Entities;

namespace ETicaretAPI_V2.Application.Abstraction.Services
{
	public interface ICommentService
	{
		Task<bool> CreateCommentAsync(CreateComment createComment);
		Task<(List<Comment>, int TotalCount, float averageRating)> GetCommentByProductIdAsync(string productId, int Size, int Page);
		Task<bool> UserHasComment(string userId, string productId);
		Task<object> UserComment(string userId, string productId);
	}
}
