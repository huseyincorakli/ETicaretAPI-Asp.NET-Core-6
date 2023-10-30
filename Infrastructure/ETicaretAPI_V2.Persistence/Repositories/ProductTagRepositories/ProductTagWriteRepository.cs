﻿using ETicaretAPI_V2.Application.Repositories.ProductTagRepositories;
using ETicaretAPI_V2.Domain.Entities;
using ETicaretAPI_V2.Persistence.Contexts;

namespace ETicaretAPI_V2.Persistence.Repositories.ProductTagRepositories
{
	public class ProductTagWriteRepository : WriteRepository<ProductTag>, IProductTagWriteRepository
	{
		public ProductTagWriteRepository(ETicaretAPI_V2DBContext context) : base(context)
		{
		}
	}
}