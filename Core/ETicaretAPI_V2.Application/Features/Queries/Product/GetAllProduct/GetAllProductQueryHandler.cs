using ETicaretAPI_V2.Application.Repositories.ProductRepositories;
using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ETicaretAPI_V2.Application.Features.Queries.Product.GetAllProduct
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryResponse>
    {
        private readonly IProductReadRepository _productReadRepository;
        readonly ILogger<GetAllProductQueryHandler> _logger;

        public GetAllProductQueryHandler(IProductReadRepository productReadRepository, ILogger<GetAllProductQueryHandler> logger)
        {
            _productReadRepository = productReadRepository;
            _logger = logger;
        }

        public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Tüm PRODUCTLAR LİSTLENEDİ!");
            int size = request.Size;
            int skip = request.Size * request.Page;
            if (!string.IsNullOrEmpty(request.ProductName) || request.FirstPriceValue!=null || request.SecondPriceValue!=null ||(request.FirstPriceValue!=null && request.SecondPriceValue!=null) || !string.IsNullOrEmpty(request.CategoryId) )
            {
                size = 10000;
                skip = 0;
            }
            var totalProductCount = await _productReadRepository.GetAll(false).CountAsync();
            var products = await _productReadRepository.GetAll(false)
                .Where(P=>P.Category.IsActive)
                .Skip(skip)
                .Take(size)
                .Include(p=>p.ProductImageFiles)
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.Stock,
                    p.Price,
                    p.CreateDate,
                    p.UpdatedDate,
                    p.ProductImageFiles,
                    p.Category.CategoryName,
                    p.CategoryId,
                    p.Category.IsActive,
                    p.Brand,
                    p.Desciription,
                    p.ShortDesciription,
                    p.Specifications
                })
                .ToListAsync();
			if (!string.IsNullOrEmpty(request.ProductName))
			{
				products = products.Where(p => 
                p.Name.Contains(request.ProductName, StringComparison.OrdinalIgnoreCase) || p.Brand.Contains(request.ProductName, StringComparison.OrdinalIgnoreCase)).ToList();
			}
			if (request.FirstPriceValue != null && request.SecondPriceValue != null)
			{
				products = products.Where(p => p.Price >= request.FirstPriceValue && p.Price <= request.SecondPriceValue).ToList();
			}
			else if (request.FirstPriceValue != null)
			{
				products = products.Where(p => p.Price >= request.FirstPriceValue).ToList();
			}
			else if (request.SecondPriceValue != null)
			{
				products = products.Where(p => p.Price <= request.SecondPriceValue).ToList();
			}

			if (!string.IsNullOrEmpty(request.CategoryId))
			{
				products = products.Where(p => p.CategoryId == Guid.Parse(request.CategoryId)).ToList();
			}

			var response = new GetAllProductQueryResponse
            {
                TotalProductCount = totalProductCount,
                Products = products
            };

            return response;
        }
    }
}
