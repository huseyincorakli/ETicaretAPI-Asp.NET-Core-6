using ETicaretAPI_V2.Domain.Entities.Common;

namespace ETicaretAPI_V2.Domain.Entities
{
	public class HomeSetting:BaseEntitiy
	{
		public string WelcomeTitle { get; set; }

		public string WelcomeText { get; set; }
		public int NumberOfFeaturedProducts { get; set; } = 6;
		public string ContactNumber { get; set; }
		public string ContactAddress { get; set; }
		public string ContactMail { get; set; }

	}
}
