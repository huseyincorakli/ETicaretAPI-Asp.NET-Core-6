using ETicaretAPI_V2.Application.DTOs.Comment;

namespace ETicaretAPI_V2.Application.Features.Queries.Comment.GetCommentByProductId
{
	public class GetCommentByProductIdQueryResponse
	{
		public int TotalCount { get; set; }
		public float AvarageScore { get; set; }

		public object ResponseData { get; set; }
	}
}
