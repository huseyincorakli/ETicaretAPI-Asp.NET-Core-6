using ETicaretAPI_V2.Domain.Entities.Common;

namespace ETicaretAPI_V2.Domain.Entities
{
	public class ShippingCompany:BaseEntitiy
	{
		public string  CompanyName { get; set; }
		public string CompanyUrl { get; set; }
		public ICollection<CompletedOrder>? CompletedOrders { get; set; }
	}
}
