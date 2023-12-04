using ETicaretAPI_V2.Application.Abstraction.Services;
using MediatR;

namespace ETicaretAPI_V2.Application.Features.Commands.Comment.AddComment
{
	public class AddCommentCommandHandler : IRequestHandler<AddCommentCommandRequest, AddCommentCommandResponse>
	{
		readonly ICommentService _commentService;

		public AddCommentCommandHandler(ICommentService commentService)
		{
			_commentService = commentService;
		}

		public async Task<AddCommentCommandResponse> Handle(AddCommentCommandRequest request, CancellationToken cancellationToken)
		{
			var data = await _commentService.CreateCommentAsync(request.CreateComment);

			if (data==true)
			{
				return new()
				{
					IsSuccess = true,
				};
			}
			else
				return new() { IsSuccess= false };
		}
	}
}
