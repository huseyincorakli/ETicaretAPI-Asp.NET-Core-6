using ETicaretAPI_V2.Application.DTOs.ShippingCompany;
using MediatR;

namespace ETicaretAPI_V2.Application.Features.Commands.ShippingCompany.CreateCompany
{
	public class CreateShippingCompanyCommandRequest:IRequest<CreateShippingCompanyCommandResponse>
	{
		public CreateShippingCompany CreateShippingCompany  { get; set; }
	}
}
