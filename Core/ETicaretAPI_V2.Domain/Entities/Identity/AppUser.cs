using Microsoft.AspNetCore.Identity;

namespace ETicaretAPI_V2.Domain.Entities.Identity
{
    public class AppUser:IdentityUser<string>
    {
        public string NameSurname { get; set; }
    }
}
