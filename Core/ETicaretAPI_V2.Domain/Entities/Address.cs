using ETicaretAPI_V2.Domain.Entities.Common;
using ETicaretAPI_V2.Domain.Entities.Identity;

namespace ETicaretAPI_V2.Domain.Entities
{
	public class Address:BaseEntitiy
	{
		public string NameSurname { get; set; }
		public string UserId { get; set; }
		public string TelNumber { get; set; }
		public string City { get; set; }
		public string County { get; set; }
		public string AddressInfo { get; set; }
		public string Directions { get; set; }
		public AppUser AppUser { get; set; }
	}
}
