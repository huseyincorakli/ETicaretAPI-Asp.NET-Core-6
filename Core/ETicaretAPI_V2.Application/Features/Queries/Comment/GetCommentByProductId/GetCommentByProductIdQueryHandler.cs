using ETicaretAPI_V2.Application.Abstraction.Services;
using MediatR;
using C = ETicaretAPI_V2.Domain.Entities;

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
			(List<C.Comment>,int totalCount,float avarageScore) data = await _commentService.GetCommentByProductIdAsync(request.ProductId,request.Size,request.Page);
			if (data.totalCount==null)
			{
				return null;
			}
			return new()
			{
				ResponseData=data.Item1,
				TotalCount=(data).totalCount,
				AvarageScore=data.avarageScore
			};
		}
	}
}
