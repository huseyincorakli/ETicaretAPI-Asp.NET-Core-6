using ETicaretAPI_V2.Domain.Entities.Common;

namespace ETicaretAPI_V2.Domain.Entities
{
	public class Message:BaseEntitiy
	{
		public string Email { get; set; }
		public string MessageTitle { get; set; }
		public string MessageContent { get; set; }
	}
}
