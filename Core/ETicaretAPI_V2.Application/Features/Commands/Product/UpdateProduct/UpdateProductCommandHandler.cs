using ETicaretAPI_V2.Application.Repositories.ProductRepositories;
using MediatR;
using P = ETicaretAPI_V2.Domain.Entities;

namespace ETicaretAPI_V2.Application.Features.Commands.Product.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        IProductReadRepository _productReadRepository;
        IProductWriteRepository _productWriteRepository;

        public UpdateProductCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }

        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            P.Product product = await _productReadRepository.GetByIdAsync(request.Id);
            product.Stock = request.Stock;
            product.Price = request.Price;
            product.Name = request.Name;
            product.Desciription = request.Description;
            product.ShortDesciription = request.ShortDesciription;
            product.Brand= request.Brand;
            product.Specifications = request.Spesification;
            product.UpdatedDate= DateTime.UtcNow;
            await _productWriteRepository.SaveAsync();
            return new UpdateProductCommandResponse() { };
        }
    }
}
