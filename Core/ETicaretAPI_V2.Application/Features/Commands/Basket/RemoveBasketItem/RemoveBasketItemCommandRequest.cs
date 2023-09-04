using MediatR;

namespace ETicaretAPI_V2.Application.Features.Commands.Basket.RemoveBasketItem
{
    public class RemoveBasketItemCommandRequest:IRequest<RemoveBasketItemCommandResponse>
    {
        public string  BasketItemId { get; set; }
    }
}
