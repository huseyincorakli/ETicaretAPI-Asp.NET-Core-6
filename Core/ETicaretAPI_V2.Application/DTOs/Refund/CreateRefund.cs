namespace ETicaretAPI_V2.Application.DTOs.Refund
{
	public class CreateRefund
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public string OrderCode { get; set; }
		public string Reason { get; set; }
	}
}
