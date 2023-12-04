using ETicaretAPI_V2.Application.Repositories.CommentRepositories;
using ETicaretAPI_V2.Domain.Entities;
using ETicaretAPI_V2.Persistence.Contexts;

namespace ETicaretAPI_V2.Persistence.Repositories.CommentRepositories
{
	public class CommentReadRepository : ReadRepository<Comment>, ICommentReadRepository
	{
		public CommentReadRepository(ETicaretAPI_V2DBContext context) : base(context)
		{
		}
	}
}
