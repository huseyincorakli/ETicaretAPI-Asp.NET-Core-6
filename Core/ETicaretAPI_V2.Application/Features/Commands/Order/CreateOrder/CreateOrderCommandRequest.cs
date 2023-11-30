﻿using MediatR;

namespace ETicaretAPI_V2.Application.Features.Commands.Order.CreateOrder
{
    public class CreateOrderCommandRequest:IRequest<CreateOrderCommandResponse>
    {

        public string Description { get; set; }
        public string Address { get; set; }
    }
}
