namespace ETicaretAPI_V2.Application.DTOs.Comment
{
	public class GetComment
	{
		public string ProductId { get; set; }
		public string UserNameSurname { get; set; }
		public string CommentTitle { get; set; }
		public string CommentContent { get; set; }
		public float Score { get; set; }
		public string CommentId { get; set; }
		public DateTime CreateDate { get; set; }
		public DateTime  UpdateDate { get; set; }
	}
}
