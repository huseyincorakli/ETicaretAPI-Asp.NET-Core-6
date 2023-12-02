using ETicaretAPI_V2.Application.ViewModels.Products;

namespace ETicaretAPI_V2.Application.Features.Queries.Product.GetBestSellingProduct
{
	public class GetBestSellingProductQueryResponse
	{
		public List<VM_Get_Best_Selling_Products> BestSellingProducts { get; set; }
	}
}
