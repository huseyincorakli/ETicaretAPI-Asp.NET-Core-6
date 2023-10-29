namespace ETicaretAPI_V2.Application.DTOs.Category
{
	public class CreateCategory
	{
		public string CategoryName { get; set; }
		public bool IsActive { get; set; } = true;
	}
}
