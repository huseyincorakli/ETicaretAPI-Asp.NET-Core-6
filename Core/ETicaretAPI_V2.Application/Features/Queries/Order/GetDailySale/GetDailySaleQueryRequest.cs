using MediatR;

namespace ETicaretAPI_V2.Application.Features.Queries.Order.GetDailySale
{
	public class GetDailySaleQueryRequest:IRequest<GetDailySaleQueryResponse>
	{
		public int Year { get; set; }
		public int Month { get; set; }
		public int Day { get; set; }
	}
}
