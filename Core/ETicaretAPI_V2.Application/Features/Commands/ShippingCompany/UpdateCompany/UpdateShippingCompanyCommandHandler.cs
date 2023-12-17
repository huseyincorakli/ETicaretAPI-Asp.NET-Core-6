using ETicaretAPI_V2.Application.Abstraction.Services;
using MediatR;

namespace ETicaretAPI_V2.Application.Features.Commands.ShippingCompany.UpdateCompany
{
	public class UpdateShippingCompanyCommandHandler : IRequestHandler<UpdateShippingCompanyCommandRequest, UpdateShippingCompanyCommandResponse>
	{
		readonly IShippingCompanyService _shippingCompanyService;

		public UpdateShippingCompanyCommandHandler(IShippingCompanyService shippingCompanyService)
		{
			_shippingCompanyService = shippingCompanyService;
		}

		public async Task<UpdateShippingCompanyCommandResponse> Handle(UpdateShippingCompanyCommandRequest request, CancellationToken cancellationToken)
		{
			var response=  await _shippingCompanyService.UpdateCompanyAsync(new()
			{
				CompanyName = request.UpdateShippingCompany.CompanyName,
				CompanyUrl = request.UpdateShippingCompany.CompanyUrl,
			},request.CompanyId);

			return new() { IsSuccess = response };
		}
	}
}
