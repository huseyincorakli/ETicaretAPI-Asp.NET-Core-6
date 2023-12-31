﻿using MediatR;

namespace ETicaretAPI_V2.Application.Features.Commands.Order.CompleteOrder
{
    public class CompleteOrderCommandRequest:IRequest<CompleteOrderCommandResponse>
    {
        public string  Id { get; set; }
        public string CompanyId { get; set; }
        public string TrackCode { get; set; }
    }
}
