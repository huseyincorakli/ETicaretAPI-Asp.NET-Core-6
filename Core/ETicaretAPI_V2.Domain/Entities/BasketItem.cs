using ETicaretAPI_V2.Domain.Entities.Common;

namespace ETicaretAPI_V2.Domain.Entities
{
    public class BasketItem:BaseEntitiy
    {
        public Guid ProductId { get; set; }
        public Guid BasketId { get; set; }
        public int  Quantity { get; set; }
        public Basket Basket { get; set; }
        public Product Product { get; set; }
    }
}
