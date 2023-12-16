using MediatR;

namespace ETicaretAPI_V2.Application.Features.Commands.Comment.SummarizeComment
{
	public class SummarizeCommentCommandRequest:IRequest<SummarizeCommentCommandResponse>
	{
		public string ProductId { get; set; }
	}
}
