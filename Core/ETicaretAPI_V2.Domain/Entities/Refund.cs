using ETicaretAPI_V2.Domain.Entities.Common;

namespace ETicaretAPI_V2.Domain.Entities
{
	public class Refund:BaseEntitiy
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public string OrderCode { get; set; }
		public string Reason { get; set; }
		public string ReturnStatus { get; set; }
	}
}

