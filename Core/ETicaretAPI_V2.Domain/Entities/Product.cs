using ETicaretAPI_V2.Domain.Entities.Common;

namespace ETicaretAPI_V2.Domain.Entities
{
    public class Product:BaseEntitiy
    {
        public string Name { get; set; }
        public string ShortDesciription { get; set; }
        public string Brand { get; set; }
        public int  Stock { get; set; }
        public int QuantitySold { get; set; } = 0;
        public float Price { get; set; }
        public string  Desciription { get; set; }
        public string[]?  Specifications { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        //public ICollection<Order> Orders { get; set; }
        public ICollection<BasketItem> BasketItems { get; set; }
        public ICollection<ProductImageFile> ProductImageFiles { get; set; }
        public ICollection<Comment>? Comments { get; set; }
    }
}
