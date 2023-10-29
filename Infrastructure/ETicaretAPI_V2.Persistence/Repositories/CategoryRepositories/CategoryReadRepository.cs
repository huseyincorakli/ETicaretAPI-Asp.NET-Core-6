using ETicaretAPI_V2.Application.Repositories.CategoryRepositories;
using ETicaretAPI_V2.Domain.Entities;
using ETicaretAPI_V2.Persistence.Contexts;

namespace ETicaretAPI_V2.Persistence.Repositories.CategoryRepositories
{
	public class CategoryReadRepository : ReadRepository<Category>, ICategoryReadRepository
	{
		public CategoryReadRepository(ETicaretAPI_V2DBContext context) : base(context)
		{
		}
	}
}
