﻿using MediatR;

namespace ETicaretAPI_V2.Application.Features.Queries.Order.GetAllOrder
{
    public class GetAllOrdersQueryRequest:IRequest<GetAllOrdersQueryResponse>
    {
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 5;
    }
}