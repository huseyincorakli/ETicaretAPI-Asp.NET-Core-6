namespace ETicaretAPI_V2.Application.DTOs.Comment
{
	public class CreateComment
	{
		public string ProductId { get; set; }
		public string CommentContent { get; set; }
		public string CommentTitle { get; set; }
		public string NameSurname { get; set; }
		public float Score { get; set; }
		public string UserId { get; set; }
	}
}
