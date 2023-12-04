using ETicaretAPI_V2.Application.Abstraction.Services;
using MediatR;

namespace ETicaretAPI_V2.Application.Features.Queries.Comment.GetCommentByProductId
{
	public class GetCommentByProductIdQueryHandler : IRequestHandler<GetCommentByProductIdQueryRequest, GetCommentByProductIdQueryResponse>
	{
		readonly ICommentService _commentService;

		public GetCommentByProductIdQueryHandler(ICommentService commentService)
		{
			_commentService = commentService;
		}

		public async Task<GetCommentByProductIdQueryResponse> Handle(GetCommentByProductIdQueryRequest request, CancellationToken cancellationToken)
		{
			object data = await _commentService.GetCommentByProductIdAsync(request.ProductId);

			return new()
			{
				ResponseData = data
			};
		}
	}
}
