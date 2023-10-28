using ETicaretAPI_V2.Domain.Entities.Common;

namespace ETicaretAPI_V2.Domain.Entities
{
	public class ProductTag:BaseEntitiy
	{
		public string TagName { get; set; }
		public Guid CategoryId { get; set; }
		public bool IsActive { get; set; }
		public Category Category { get; set; }
		public ICollection<Product> Products { get; set; }
	}
}
