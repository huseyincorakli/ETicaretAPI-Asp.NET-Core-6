namespace ETicaretAPI_V2.Application.DTOs.Order
{
    public class CompletedOrderDTO
    {
        public string  OrderCode { get; set; }
        public DateTime OrderDate { get; set; }
        public string Username { get; set; }
        public string UserSurname { get; set; }
        public string Email { get; set; }
    }
}
