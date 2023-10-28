using ETicaretAPI_V2.Domain.Entities.Common;

namespace ETicaretAPI_V2.Domain.Entities
{
	public class Category :BaseEntitiy
	{
		public string CategoryName { get; set; }
		public bool IsActive { get; set; }
		public ICollection<ProductTag> ProductTags { get; set; }
	}
}
