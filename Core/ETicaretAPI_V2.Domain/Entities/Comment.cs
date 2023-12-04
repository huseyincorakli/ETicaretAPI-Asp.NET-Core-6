using ETicaretAPI_V2.Domain.Entities.Common;

namespace ETicaretAPI_V2.Domain.Entities
{
	public class Comment:BaseEntitiy
	{
		public Guid ProductId { get; set; }
		public string UserNameSurname { get; set; }
		public string UserCommentTitle { get; set; }
		public string UserCommentContent { get; set; }
		public float UserScore { get; set; }
		public Product Product { get; set; }
	}
}
