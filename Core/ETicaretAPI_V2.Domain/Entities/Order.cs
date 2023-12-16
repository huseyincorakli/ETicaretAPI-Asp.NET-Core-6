using ETicaretAPI_V2.Domain.Entities.Common;

namespace ETicaretAPI_V2.Domain.Entities
{
    public class Order:BaseEntitiy
    {
      
        public string Description { get; set; }
        public string Address { get; set; }
        public Basket Basket { get; set; }
        public string OrderCode { get; set; }
        public CompletedOrder CompletedOrder { get; set; }
		public Guid? ShippingCompanyId { get; set; }
        public string? CargoTrackingCode { get; set; }
        public ShippingCompany? ShippingCompany { get; set; }

    }
}
