using ETicaretAPI_V2.Application.Abstraction.Services;
using MediatR;

namespace ETicaretAPI_V2.Application.Features.Commands.ShippingCompany.CreateCompany
{
	public class CreateShippingCompanyCommandHandler : IRequestHandler<CreateShippingCompanyCommandRequest, CreateShippingCompanyCommandResponse>
	{
		readonly IShippingCompanyService _shippingCompanyService;

		public CreateShippingCompanyCommandHandler(IShippingCompanyService shippingCompanyService)
		{
			_shippingCompanyService = shippingCompanyService;
		}

		async Task<CreateShippingCompanyCommandResponse> IRequestHandler<CreateShippingCompanyCommandRequest, CreateShippingCompanyCommandResponse>.Handle(CreateShippingCompanyCommandRequest request, CancellationToken cancellationToken)
		{
			bool isSuccess = await _shippingCompanyService.AddCompanyAsync(new()
			{
				CompanyName = request.CreateShippingCompany.CompanyName,
				CompanyUrl = request.CreateShippingCompany.CompanyUrl,
				CreateDate = DateTime.UtcNow,
				Id = Guid.NewGuid()
			});

			if (isSuccess)
			{
				return new()
				{
					IsSuccess = true,
				};
			}
			else
			{
				return new() { IsSuccess = false };
			}

		}
	}
}
