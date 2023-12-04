using MediatR;

namespace ETicaretAPI_V2.Application.Features.Queries.Comment.GetCommentByProductId
{
	public class GetCommentByProductIdQueryRequest:IRequest<GetCommentByProductIdQueryResponse>
	{
		public int Size { get; set; } = 5;
		public int Page { get; set; } = 0;
		public string ProductId { get; set; }
	}
}
