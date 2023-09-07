namespace ETicaretAPI_V2.Application.DTOs.Order
{
    public class CreateOrder
    {
        public string Description { get; set; }
        public string Address { get; set; }
        public string? BasketId { get; set; }
    }
}
