using ETicaretAPI_V2.Application.Abstraction.Storage;
using ETicaretAPI_V2.Application.Repositories.ProductImageFileRepositories;
using ETicaretAPI_V2.Application.Repositories.ProductRepositories;
using MediatR;
using P = ETicaretAPI_V2.Domain.Entities;

namespace ETicaretAPI_V2.Application.Features.Commands.ProductImageFile.UploadProductImage
{
    public class UploadProductImageCommandHandler : IRequestHandler<UploadProductImageCommandRequest, UploadProductImageCommandResponse>
    {
        IStorageService _storageService;
        IProductReadRepository _productReadRepository;
        IProductImageFileWriteRepository _productImageFileWriteRepository;

        public UploadProductImageCommandHandler(IStorageService storageService, IProductReadRepository productReadRepository, IProductImageFileWriteRepository productImageFileWriteRepository)
        {
            _storageService = storageService;
            _productReadRepository = productReadRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
        }

        public async Task<UploadProductImageCommandResponse> Handle(UploadProductImageCommandRequest request, CancellationToken cancellationToken)
        {
            var datas = await _storageService.UploadAsync(request.Files, "photo-images");
            P.Product product = await _productReadRepository.GetByIdAsync(request.Id);
            await _productImageFileWriteRepository.AddRangeAsync(datas.Select(data => new P.ProductImageFile()
            {
                FileName = data.fileName,
                Path = data.pathOrContainer,
                Storage = _storageService.StorageName,
                Products = new List<P.Product>() { product }

            }).ToList());
            await _productImageFileWriteRepository.SaveAsync();
            return new UploadProductImageCommandResponse();
        }
    }
}
