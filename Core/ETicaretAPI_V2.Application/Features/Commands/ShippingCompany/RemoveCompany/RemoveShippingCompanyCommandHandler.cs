using ETicaretAPI_V2.Application.Abstraction.Services;
using MediatR;

namespace ETicaretAPI_V2.Application.Features.Commands.ShippingCompany.RemoveCompany
{
	public class RemoveShippingCompanyCommandHandler : IRequestHandler<RemoveShippingCompanyCommandRequest, RemoveShippingCompanyCommandResponse>
	{
		readonly IShippingCompanyService _shippingCompanyService;

		public RemoveShippingCompanyCommandHandler(IShippingCompanyService shippingCompanyService)
		{
			_shippingCompanyService = shippingCompanyService;
		}

		async Task<RemoveShippingCompanyCommandResponse> IRequestHandler<RemoveShippingCompanyCommandRequest, RemoveShippingCompanyCommandResponse>.Handle(RemoveShippingCompanyCommandRequest request, CancellationToken cancellationToken)
		{
			bool isSucces = await _shippingCompanyService.RemoveCompanyAsync(request.CompanyId);

			return new() { IsSuccess = isSucces };


		}

	}
}
