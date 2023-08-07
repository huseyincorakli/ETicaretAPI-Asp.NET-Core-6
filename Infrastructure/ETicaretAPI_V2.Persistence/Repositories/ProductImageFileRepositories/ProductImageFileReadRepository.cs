using ETicaretAPI_V2.Application.Repositories.ProductImageFileRepositories;
using ETicaretAPI_V2.Domain.Entities;
using ETicaretAPI_V2.Persistence.Contexts;


namespace ETicaretAPI_V2.Persistence.Repositories.ProductImageFileRepositories
{
    public class ProductImageFileReadRepository : ReadRepository<ProductImageFile>, IProductImageFileReadRepository
    {
        public ProductImageFileReadRepository(ETicaretAPI_V2DBContext context) : base(context)
        {
        }
    }
}
