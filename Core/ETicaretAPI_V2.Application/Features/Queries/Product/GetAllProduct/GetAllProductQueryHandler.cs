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
            if (!string.IsNullOrEmpty(request.ProductName))
            {
                size = 300;
                skip = 0;
            }
            var totalProductCount = await _productReadRepository.GetAll(false).CountAsync();
            var products = await _productReadRepository.GetAll(false)
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
                    p.Category.CategoryName
                })
                .ToListAsync();
			if (!string.IsNullOrEmpty(request.ProductName))
			{
				products = products.Where(p => p.Name.Contains(request.ProductName, StringComparison.OrdinalIgnoreCase)).ToList();
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
