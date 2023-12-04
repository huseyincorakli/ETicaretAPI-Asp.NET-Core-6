using MediatR;

namespace ETicaretAPI_V2.Application.Features.Queries.Order.GetUnCompletedOrders
{
	public class GetUnCompletedOrdersQueryRequest:IRequest<GetUnCompletedOrdersQueryResponse>
	{
		public int Size { get; set; }
	}
}
