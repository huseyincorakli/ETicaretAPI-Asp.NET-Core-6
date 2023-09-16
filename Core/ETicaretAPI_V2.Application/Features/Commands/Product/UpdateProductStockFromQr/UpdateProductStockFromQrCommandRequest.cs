using MediatR;

namespace ETicaretAPI_V2.Application.Features.Commands.Product.UpdateProductStockFromQr
{
    public class UpdateProductStockFromQrCommandRequest:IRequest<UpdateProductStockFromQrCommandResponse>
    {
        public string ProductId { get; set; }
        public int Stock { get; set; }
    }
}