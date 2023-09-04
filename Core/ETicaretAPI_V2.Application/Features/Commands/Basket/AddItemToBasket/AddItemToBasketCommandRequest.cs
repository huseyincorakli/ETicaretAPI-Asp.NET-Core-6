using MediatR;

namespace ETicaretAPI_V2.Application.Features.Commands.Basket.AddItemToBasket
{
    public class AddItemToBasketCommandRequest:IRequest<AddItemToBasketCommandResponse>
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}