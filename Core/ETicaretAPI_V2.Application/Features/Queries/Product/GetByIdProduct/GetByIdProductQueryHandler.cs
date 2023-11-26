using ETicaretAPI_V2.Application.Repositories.CategoryRepositories;
using ETicaretAPI_V2.Application.Repositories.ProductImageFileRepositories;
using ETicaretAPI_V2.Application.Repositories.ProductRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using P = ETicaretAPI_V2.Domain.Entities;
namespace ETicaretAPI_V2.Application.Features.Queries.Product.GetByIdProduct
{
    public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
    {

        IProductReadRepository _productReadRepository;
        ICategoryReadRepository _categoryReadRepository;
        IProductImageFileReadRepository _productImageFileReadRepository;

		public GetByIdProductQueryHandler(IProductReadRepository productReadRepository, ICategoryReadRepository categoryReadRepository, IProductImageFileReadRepository productImageFileReadRepository)
		{
			_productReadRepository = productReadRepository;
			_categoryReadRepository = categoryReadRepository;
			_productImageFileReadRepository = productImageFileReadRepository;
		}

		public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
        {
			P.Product product = await _productReadRepository.GetAll()
		        .Include(p => p.ProductImageFiles)
		        .Include(p => p.Category) // Include Category if needed
		        .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));
			List<P.ProductImageFile> imageFiles = product.ProductImageFiles.ToList();

			P.Category category= await _categoryReadRepository.GetByIdAsync((product.CategoryId).ToString(),false);
			return new GetByIdProductQueryResponse {
				Name = product.Name,
				Price = product.Price,
				Stock = product.Stock,
				CategoryName = category.CategoryName,
				Description = product.Desciription,
				Brand= product.Brand,
				ShortDesciription= product.ShortDesciription,
				Specifications=product.Specifications,
				ImageFiles = imageFiles.Select(imageFile => new
				{
					FileName = imageFile.FileName,
					Storage = imageFile.Storage,
					Path = imageFile.Path,
					Showcase = imageFile.Showcase
				}).ToList()
		   };
        }
    }
}
