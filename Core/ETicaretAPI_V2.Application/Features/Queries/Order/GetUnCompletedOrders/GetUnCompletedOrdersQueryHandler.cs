using ETicaretAPI_V2.Application.Abstraction.Services;
using MediatR;

namespace ETicaretAPI_V2.Application.Features.Queries.Order.GetUnCompletedOrders
{
	public class GetUnCompletedOrdersQueryHandler : IRequestHandler<GetUnCompletedOrdersQueryRequest, GetUnCompletedOrdersQueryResponse>
	{
		readonly IOrderService _orderService;

		public GetUnCompletedOrdersQueryHandler(IOrderService orderService)
		{
			_orderService = orderService;
		}

		public async Task<GetUnCompletedOrdersQueryResponse> Handle(GetUnCompletedOrdersQueryRequest request, CancellationToken cancellationToken)
		{
			var datas	=  await _orderService.GetUnCompletedOrders(request.Size);

			return new()
			{
				UnCompletedDatas = datas
			};
		}
	}
}
