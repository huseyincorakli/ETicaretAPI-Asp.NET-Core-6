using ETicaretAPI_V2.Application.Abstraction.Services;
using ETicaretAPI_V2.Application.Repositories.ProductRepositories;
using ETicaretAPI_V2.Domain.Entities;
using System.Text.Json;

namespace ETicaretAPI_V2.Persistence.Services
{
    public class ProductService : IProductService
    {
        readonly IProductReadRepository _productReadRepository;
        readonly IQRCodeService _qrCodeService;

        public ProductService(IProductReadRepository productReadRepository, IQRCodeService qrCodeService)
        {
            _productReadRepository = productReadRepository;
            _qrCodeService = qrCodeService;
        }

        public async Task<byte[]> QRCodeToProductAsync(string productId)
        {
            
            Product product = await _productReadRepository.GetByIdAsync(productId);
            if (product==null)
                throw new Exception("PRODUCT NOT FOUND");

            var plainObject = new
            {
                product.Id,
                product.Name,
                product.Stock,
                product.Price,
                product.CreateDate
            };
            string plainText = JsonSerializer.Serialize(plainObject);

           return _qrCodeService.GenerateQRCode(plainText);
            

        }
    }
}
