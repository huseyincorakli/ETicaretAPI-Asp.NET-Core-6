using ETicaretAPI_V2.Application.Repositories.DailySaleRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ETicaretAPI_V2.Application.Features.Queries.Order.GetDailySale
{
	public class GetDailySaleQueryHandler : IRequestHandler<GetDailySaleQueryRequest, GetDailySaleQueryResponse>
	{
		readonly IDailySaleReadRepository _repository;

		public GetDailySaleQueryHandler(IDailySaleReadRepository repository)
		{
			_repository = repository;
		}

		public async Task<GetDailySaleQueryResponse> Handle(GetDailySaleQueryRequest request, CancellationToken cancellationToken)
		{
			var currentDateUtc = new DateTime(request.Year,request.Month,request.Day,0,0,0,0, DateTimeKind.Utc);
			var date = currentDateUtc.Date;
			var data = await _repository.GetAll().FirstOrDefaultAsync(a => a.SalesTime.Date == date);

			if (data==null)
			{
				return new()
				{
					SaleQuantity = 0
				};
			}
			return new()
			{
				SaleQuantity = data.SaleQuantity
			};
		}
	}
}
