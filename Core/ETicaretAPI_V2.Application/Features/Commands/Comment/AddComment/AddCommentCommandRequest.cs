using ETicaretAPI_V2.Application.DTOs.Comment;
using MediatR;

namespace ETicaretAPI_V2.Application.Features.Commands.Comment.AddComment
{
	public class AddCommentCommandRequest:IRequest<AddCommentCommandResponse>
	{
		public CreateComment CreateComment { get; set; }
	}
}
