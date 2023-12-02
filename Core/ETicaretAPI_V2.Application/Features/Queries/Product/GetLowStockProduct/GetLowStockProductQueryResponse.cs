using ETicaretAPI_V2.Application.ViewModels.Products;

namespace ETicaretAPI_V2.Application.Features.Queries.Product.GetLowStockProduct
{
	public class GetLowStockProductQueryResponse
	{
		public List<VM_Get_Low_Stock_Products> LowStockProducts{ get; set; }
	}
}
