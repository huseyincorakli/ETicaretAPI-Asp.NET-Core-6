using MediatR;

namespace ETicaretAPI_V2.Application.Features.Queries.Basket.GetBasketItems
{
    public class GetBasketItemsQueryRequest : IRequest<List<GetBasketItemsQueryResponse>>
    {
        public string?  CampaignCode { get; set; }
        public string? UserId { get; set; }
    }
}
