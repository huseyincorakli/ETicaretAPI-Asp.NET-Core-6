using ETicaretAPI_V2.Domain.Entities;

namespace ETicaretAPI_V2.Application.Repositories.ProductRepositories
{
    public interface IProductReadRepository:IReadRepository<Product>
    {
		Task<List<LowestStockProduct>> GetTop5LowestStockProductsAsync();
		Task<List<BestSellingProduct>> GetSellingProductsAsync();

	}

}
