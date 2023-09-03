using P = ETicaretAPI_V2.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;
using ETicaretAPI_V2.Application.Repositories.ProductRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;

namespace ETicaretAPI_V2.Application.Features.Queries.ProductImageFile.GetProductImages
{
    public class GetProductImagesQueryHandler : IRequestHandler<GetProductImagesQueryRequest, List<GetProductImagesQueryResponse>>
    {
        readonly IConfiguration _configuration;
        readonly IProductReadRepository _productReadRepository;
      
        public GetProductImagesQueryHandler(IConfiguration configuration, IProductReadRepository productReadRepository, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _productReadRepository = productReadRepository;
           
        }

        public async Task<List<GetProductImagesQueryResponse>> Handle(GetProductImagesQueryRequest request, CancellationToken cancellationToken)
        {
            P.Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles).FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));
            return product?.ProductImageFiles.Select(p => new GetProductImagesQueryResponse
            {
                Path = $"{_configuration["BaseStorageUrl"]}/{p.Path}",
                Id = p.Id,
                FileName= p.FileName,
                Showcase = p.Showcase
            }).ToList();
        }
    }
}
