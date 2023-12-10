using MediatR;

namespace ETicaretAPI_V2.Application.Features.Queries.Comment.UserHasComment
{
	public class UserHasCommentQueryRequest:IRequest<UserHasCommentQueryResponse>
	{
		public string UserId { get; set; }
		public string ProductId { get; set; }
	}
}
