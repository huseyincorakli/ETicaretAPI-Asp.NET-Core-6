using ETicaretAPI_V2.Domain.Entities.Common;
using ETicaretAPI_V2.Domain.Entities.Identity;

namespace ETicaretAPI_V2.Domain.Entities
{
    public class Basket : BaseEntitiy
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public Order Order { get; set; }
        public ICollection<BasketItem> BasketItems { get; set; }
    }
}
