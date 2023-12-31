﻿using ETicaretAPI_V2.Domain.Entities.Common;

namespace ETicaretAPI_V2.Domain.Entities
{
    public class CompletedOrder:BaseEntitiy
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
		public Guid? ShippingCompanyId { get; set; }
		public string? CargoTrackingCode { get; set; }
		public ShippingCompany? ShippingCompany { get; set; }
	}
}
