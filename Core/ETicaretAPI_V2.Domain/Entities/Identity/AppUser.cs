using Microsoft.AspNetCore.Identity;

namespace ETicaretAPI_V2.Domain.Entities.Identity
{
    public class AppUser : IdentityUser<string>
    {
        public string NameSurname { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenEndDate { get; set; }
        public Address? Address { get; set; }
        public ICollection<Basket> Baskets { get; set; }
    }
}
