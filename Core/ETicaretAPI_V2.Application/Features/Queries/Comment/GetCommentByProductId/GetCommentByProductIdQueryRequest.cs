using MediatR;

namespace ETicaretAPI_V2.Application.Features.Queries.Comment.GetCommentByProductId
{
	public class GetCommentByProductIdQueryRequest:IRequest<GetCommentByProductIdQueryResponse>
	{
		public string ProductId { get; set; }
	}
}
