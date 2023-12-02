using ETicaretAPI_V2.Domain.Entities.Common;

namespace ETicaretAPI_V2.Domain.Entities
{
	public class DailySale:BaseEntitiy
	{
		public string ProductId { get; set; }
		public DateTime SaleDate { get; set; }
		public int QuantitySold { get; set; }
	}
}
