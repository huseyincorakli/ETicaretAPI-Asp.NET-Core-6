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
		public async Task<UserHasCommentQueryResponse> Handle(UserHasCommentQueryRequest request, CancellationToken cancellationToken)
		{
			if (request.UserId==null)
			{
				return new()
				{
					Comment = null,
					isHas = false,
				};
			}
		 bool response = await	_commentService.UserHasComment(request.UserId, request.ProductId);
			if (response)
			{
				var data= await _commentService.UserComment(request.UserId, request.ProductId);
				return new()
				{
					isHas = true,
					Comment = data
				};
			}
			else
				return new() {
					isHas = false,
					Comment = null
				};
		}
	}
}
