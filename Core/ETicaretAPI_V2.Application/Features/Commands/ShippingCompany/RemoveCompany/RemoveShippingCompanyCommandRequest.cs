using MediatR;

namespace ETicaretAPI_V2.Application.Features.Commands.ShippingCompany.RemoveCompany
{
	public class RemoveShippingCompanyCommandRequest:IRequest<RemoveShippingCompanyCommandResponse>
	{
		public string CompanyId { get; set; }
	}
}
