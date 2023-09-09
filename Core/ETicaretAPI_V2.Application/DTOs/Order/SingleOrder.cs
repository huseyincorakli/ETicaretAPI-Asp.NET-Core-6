using ETicaretAPI_V2.Domain.Entities;

namespace ETicaretAPI_V2.Application.DTOs.Order
{
    public class SingleOrder
    {

        public string  Id { get; set; }
        public string Address { get; set; }
        public object BasketItems { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string OrderCode { get; set; }
    }
}
