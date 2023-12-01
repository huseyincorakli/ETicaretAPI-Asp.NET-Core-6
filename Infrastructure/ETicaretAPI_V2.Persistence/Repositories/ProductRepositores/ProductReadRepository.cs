using ETicaretAPI_V2.Application.Repositories.ProductRepositories;
using ETicaretAPI_V2.Domain.Entities;
using ETicaretAPI_V2.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI_V2.Persistence.Repositories.ProductRepositores
{
    public class ProductReadRepository : ReadRepository<Product>, IProductReadRepository
    {
		public readonly DbContext _dbContext;

		public ProductReadRepository(ETicaretAPI_V2DBContext context) : base(context)
        {
			_dbContext = context;
        }
		public async Task<List<LowestStockProduct>> GetTop5LowestStockProductsAsync()
		{
			return await _dbContext.Set<LowestStockProduct>()
				.FromSqlRaw("select * from get_top5_lowest_stock_products();")
				.ToListAsync();
		}
		public async Task<List<BestSellingProduct>> GetSellingProductsAsync()
		{
			return await _dbContext.Set<BestSellingProduct>()
				.FromSqlRaw("SELECT * FROM get_best_selling_products()")
				.ToListAsync();
		}



	}
}
