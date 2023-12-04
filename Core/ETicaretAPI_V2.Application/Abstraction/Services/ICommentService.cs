using ETicaretAPI_V2.Application.DTOs.Comment;

namespace ETicaretAPI_V2.Application.Abstraction.Services
{
	public interface ICommentService
	{
		Task<bool> CreateCommentAsync(CreateComment createComment);
		Task<object> GetCommentByProductIdAsync(string productId);
		Task<object> GetCommentByUserIdAsync(string userId);
	}
}
