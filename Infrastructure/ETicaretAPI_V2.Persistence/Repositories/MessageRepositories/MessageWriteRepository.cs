using ETicaretAPI_V2.Application.Repositories.MessageRepositories;
using ETicaretAPI_V2.Domain.Entities;
using ETicaretAPI_V2.Persistence.Contexts;

namespace ETicaretAPI_V2.Persistence.Repositories.MessageRepositories
{
	public class MessageWriteRepository : WriteRepository<Message>, IMessageWriteRepository
	{
		public MessageWriteRepository(ETicaretAPI_V2DBContext context) : base(context)
		{
		}
	}
}
