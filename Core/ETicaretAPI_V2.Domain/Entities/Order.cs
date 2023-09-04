using ETicaretAPI_V2.Domain.Entities.Common;

namespace ETicaretAPI_V2.Domain.Entities
{
    public class Order:BaseEntitiy
    {
      
        public string Description { get; set; }
        public string Address { get; set; }
        public Guid CustomerId { get; set; }
        public ICollection<Product> Products { get; set; }
        public Basket Basket { get; set; }
        public Customer Customer { get; set; }
    }
}
