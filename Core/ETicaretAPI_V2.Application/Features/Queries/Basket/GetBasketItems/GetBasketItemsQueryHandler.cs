using ETicaretAPI_V2.Application.Abstraction.Services;
using ETicaretAPI_V2.Application.Repositories.CampaignUsageRepositories;
using MediatR;
using CC = ETicaretAPI_V2.Domain.Entities;
namespace ETicaretAPI_V2.Application.Features.Queries.Basket.GetBasketItems
{
    public class GetBasketItemsQueryHandler : IRequestHandler<GetBasketItemsQueryRequest, List<GetBasketItemsQueryResponse>>
    {
        readonly IBasketService _basketService;
        readonly ICampaignService _campaignService;
		readonly ICampaignUsageReadRepository _campaignUsageReadRepository;

		public GetBasketItemsQueryHandler(IBasketService basketService, ICampaignService campaignService, ICampaignUsageReadRepository campaignUsageReadRepository)
		{
			_basketService = basketService;
			_campaignService = campaignService;
			_campaignUsageReadRepository = campaignUsageReadRepository;
		}

		public async Task<List<GetBasketItemsQueryResponse>> Handle(GetBasketItemsQueryRequest request, CancellationToken cancellationToken)
        {
			float discount = 0;

			if (request.CampaignCode!=null && request.UserId!=null)
            {
				var data =  await _campaignService.GetAllCampaignAsync(1,request.CampaignCode);
				CC.Campaign campaign = data[0];
				
				bool checkUsage=  await  CheckUsage(request.UserId, (campaign.Id).ToString());
				if (!checkUsage)
				{
					if (campaign != null)
					{
						discount = campaign.DiscountPercentage;
					}
				}
			}
            var basketItems = await _basketService.GetBasketItemsAsync();

            
            return basketItems.Select(ba => new GetBasketItemsQueryResponse
            {
                BasketItemId = ba.Id.ToString(),
                ProductId=ba.Product.Id.ToString(),
				ImagePath= ba.Product.ProductImageFiles?.FirstOrDefault()?.Path,
				Name = ba.Product.Name,
                Price = ba.Product.Price,
                Quantity = ba.Quantity,
                TotalPrice = CalculateTotalPrice(ba.Quantity, ba.Product.Price, discount)
			}).ToList();
        }


		private float CalculateTotalPrice(int quantity, float price, float discount)
		{
			float totalPrice = quantity * price;

			if (discount > 0)
			{
				discount = totalPrice * (discount / 100);
				totalPrice = totalPrice - discount;
				return totalPrice;

			}
			else
			{
				return totalPrice;
			}
		}
		private async Task<bool> CheckUsage(string userId, string campaignId)
		{

			var data = await _campaignUsageReadRepository.GetSingleAsync(s => s.UserId == userId && s.CampaignId == Guid.Parse(campaignId));

			if (data == null)
			{
				return false;
			}
			else
				return true;
		}
	}

}
