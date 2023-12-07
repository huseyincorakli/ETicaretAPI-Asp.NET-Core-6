using ETicaretAPI_V2.Application.Abstraction.Services;
using MediatR;

namespace ETicaretAPI_V2.Application.Features.Queries.Comment.UserHasComment
{
	public class UserHasCommentQueryHandler : IRequestHandler<UserHasCommentQueryRequest, UserHasCommentQueryResponse>
	{
		readonly ICommentService _commentService;

		public UserHasCommentQueryHandler(ICommentService commentService)
		{
			_commentService = commentService;
		}
		//burada kaldım
		public Task<UserHasCommentQueryResponse> Handle(UserHasCommentQueryRequest request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
