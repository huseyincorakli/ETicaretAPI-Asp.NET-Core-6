using ETicaretAPI_V2.Application.Abstraction.Services;
using MediatR;

namespace ETicaretAPI_V2.Application.Features.Commands.Product.UpdateProductStockFromQr
{
    public class UpdateProductStockFromQrCommandHandler : IRequestHandler<UpdateProductStockFromQrCommandRequest, UpdateProductStockFromQrCommandResponse>
    {
        readonly IProductService _productService;

        public UpdateProductStockFromQrCommandHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<UpdateProductStockFromQrCommandResponse> Handle(UpdateProductStockFromQrCommandRequest request, CancellationToken cancellationToken)
        {
            await _productService.UpdateProductStockAsync(request.ProductId, request.Stock);
            return new();
        }
    }
}
