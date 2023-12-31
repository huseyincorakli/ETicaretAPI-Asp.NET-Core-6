﻿namespace ETicaretAPI_V2.Application.Features.Queries.Basket.GetBasketItems
{
    public class GetBasketItemsQueryResponse
    {
        public string BasketItemId { get; set; }
        public string ProductId { get; set; }
        public string  Name { get; set; }
        public float Price { get; set; }
        public int  Quantity { get; set; }
        public float TotalPrice { get; set; }
        public string? ImagePath { get; set; }

    }
}
