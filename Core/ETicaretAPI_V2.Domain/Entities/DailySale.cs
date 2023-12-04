using ETicaretAPI_V2.Domain.Entities.Common;

namespace ETicaretAPI_V2.Domain.Entities
{
	public class DailySale:BaseEntitiy
	{
		public int SaleQuantity { get; set; }
		public DateTime SalesTime { get; set; }
	}
}
