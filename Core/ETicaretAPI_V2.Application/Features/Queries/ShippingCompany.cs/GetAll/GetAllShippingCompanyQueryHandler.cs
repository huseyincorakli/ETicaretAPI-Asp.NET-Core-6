using ETicaretAPI_V2.Application.Abstraction.Services;
using MediatR;

namespace ETicaretAPI_V2.Application.Features.Queries.ShippingCompany.cs.GetAll
{
	public class GetAllShippingCompanyQueryHandler : IRequestHandler<GetAllShippingCompanyQueryRequest, GetAllShippingCompanyQueryResponse>
	{
		readonly IShippingCompanyService _shippingCompanyService;

		public GetAllShippingCompanyQueryHandler(IShippingCompanyService shippingCompanyService)
		{
			_shippingCompanyService = shippingCompanyService;
		}

		public async Task<GetAllShippingCompanyQueryResponse> Handle(GetAllShippingCompanyQueryRequest request, CancellationToken cancellationToken)
		{
			var data =  await _shippingCompanyService.GetAll();
			return new() { ShippingCompanies = data };
		}
	}
}
