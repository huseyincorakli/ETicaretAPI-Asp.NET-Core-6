using ETicaretAPI_V2.Domain.Entities.Common;

namespace ETicaretAPI_V2.Domain.Entities
{
    public class Product:BaseEntitiy
    {
        public string Name { get; set; }
        public int  Stock { get; set; }
        public float Price { get; set; }
        //public ICollection<Order> Orders { get; set; }
        public ICollection<BasketItem> BasketItems { get; set; }
        public ICollection<ProductImageFile> ProductImageFiles { get; set; }
    }
}
