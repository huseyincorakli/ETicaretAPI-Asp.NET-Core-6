using ETicaretAPI_V2.Application.Abstraction.Services;
using ETicaretAPI_V2.Application.DTOs.Order;
using MediatR;

namespace ETicaretAPI_V2.Application.Features.Commands.Order.CompleteOrder
{
    public class CompleteOrderCommandHandler : IRequestHandler<CompleteOrderCommandRequest, CompleteOrderCommandResponse>
    {
        readonly IOrderService _orderService;
        readonly IMailService _mailService;
        readonly IShippingCompanyService _shippingCompanyService;

		public CompleteOrderCommandHandler(IOrderService orderService, IMailService mailService, IShippingCompanyService shippingCompanyService)
		{
			_orderService = orderService;
			_mailService = mailService;
			_shippingCompanyService = shippingCompanyService;
		}

		public async Task<CompleteOrderCommandResponse> Handle(CompleteOrderCommandRequest request, CancellationToken cancellationToken)
        {
            (bool succeeded,CompletedOrderDTO dto)  = await _orderService.CompleteOrderAsync(request.Id,request.TrackCode,request.CompanyId);
            var data = await _shippingCompanyService.GetCompanyByIdAsync(request.CompanyId);
            if (data ==null)
            {
                throw new Exception("Kargo firması boş geçilemez");
            }
            if(succeeded==true)
            {
                await _mailService.SendCompletedOrderMailAsync(
                    dto.Email, dto.OrderCode, dto.OrderDate, dto.Username, dto.UserSurname,request.TrackCode,data.CompanyName);
            }
            return new();
        }
    }
}
//