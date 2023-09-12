using ETicaretAPI_V2.Application.Abstraction.Services;
using ETicaretAPI_V2.Application.DTOs.Order;
using MediatR;

namespace ETicaretAPI_V2.Application.Features.Commands.Order.CompleteOrder
{
    public class CompleteOrderCommandHandler : IRequestHandler<CompleteOrderCommandRequest, CompleteOrderCommandResponse>
    {
        readonly IOrderService _orderService;
        readonly IMailService _mailService;

        public CompleteOrderCommandHandler(IOrderService orderService, IMailService mailService)
        {
            _orderService = orderService;
            _mailService = mailService;
        }

        public async Task<CompleteOrderCommandResponse> Handle(CompleteOrderCommandRequest request, CancellationToken cancellationToken)
        {
            (bool succeeded,CompletedOrderDTO dto)  = await _orderService.CompleteOrderAsync(request.Id);
            if(succeeded==true)
            {
                await _mailService.SendCompletedOrderMailAsync(dto.Email, dto.OrderCode, dto.OrderDate, dto.Username, dto.UserSurname);
            }
            return new();
        }
    }
}
