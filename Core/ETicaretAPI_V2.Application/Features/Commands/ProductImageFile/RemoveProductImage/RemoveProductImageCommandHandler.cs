using ETicaretAPI_V2.Application.Repositories.FileRepositories;
using ETicaretAPI_V2.Application.Repositories.ProductImageFileRepositories;
using ETicaretAPI_V2.Application.Repositories.ProductRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using P = ETicaretAPI_V2.Domain.Entities;
namespace ETicaretAPI_V2.Application.Features.Commands.ProductImageFile.RemoveProductImage
{
    public class RemoveProductImageCommandHandler : IRequestHandler<RemoveProductImageCommandRequest, RemoveProductImageCommandResponse>
    {
        readonly IProductReadRepository _productReadRepository;
        readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
        readonly IFileWriteRepository _fileWriteRepository;

        public RemoveProductImageCommandHandler(IProductReadRepository productReadRepository, IProductImageFileWriteRepository productImageFileWriteRepository, IFileWriteRepository fileWriteRepository)
        {

            _productReadRepository = productReadRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _fileWriteRepository = fileWriteRepository;
        }

        public async Task<RemoveProductImageCommandResponse> Handle(RemoveProductImageCommandRequest request, CancellationToken cancellationToken)
        {
            P.Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles).FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));
            P.ProductImageFile? productImageFile = product?.ProductImageFiles.FirstOrDefault(f => f.Id == Guid.Parse(request.ImageId));
            if(productImageFile != null)
                product?.ProductImageFiles.Remove(productImageFile);
            await _productImageFileWriteRepository.RemoveAsync(request.ImageId);
            await _fileWriteRepository.SaveAsync();
            return new RemoveProductImageCommandResponse();
        }
    }
}
