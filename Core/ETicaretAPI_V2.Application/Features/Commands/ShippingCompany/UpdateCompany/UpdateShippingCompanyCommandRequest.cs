using ETicaretAPI_V2.Application.DTOs.ShippingCompany;
using MediatR;

namespace ETicaretAPI_V2.Application.Features.Commands.ShippingCompany.UpdateCompany
{
	public class UpdateShippingCompanyCommandRequest:IRequest<UpdateShippingCompanyCommandResponse>
	{
		public UpdateShippingCompany UpdateShippingCompany { get; set; }
		public string CompanyId { get; set; }
	}
}
