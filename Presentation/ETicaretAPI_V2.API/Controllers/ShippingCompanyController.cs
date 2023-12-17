using ETicaretAPI_V2.Application.Features.Commands.ShippingCompany.CreateCompany;
using ETicaretAPI_V2.Application.Features.Commands.ShippingCompany.RemoveCompany;
using ETicaretAPI_V2.Application.Features.Commands.ShippingCompany.UpdateCompany;
using ETicaretAPI_V2.Application.Features.Queries.ShippingCompany.cs.GetAll;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI_V2.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ShippingCompanyController : ControllerBase
	{
		readonly IMediator _mediator;

		public ShippingCompanyController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> GetAllShippingCompanies([FromQuery]GetAllShippingCompanyQueryRequest getAllShippingCompanyQueryRequest)
		{
			GetAllShippingCompanyQueryResponse response = await _mediator.Send(getAllShippingCompanyQueryRequest);
			return Ok(response);
		}

		[HttpPost]
		public async Task<IActionResult> CreateShippingCompany([FromBody] CreateShippingCompanyCommandRequest createShippingCompanyCommandRequest)
		{
			CreateShippingCompanyCommandResponse response = await _mediator.Send(createShippingCompanyCommandRequest);
			return Ok(response);
		}

		[HttpDelete("[action]")]
		public async Task<IActionResult> RemoveShippingCompany([FromQuery] RemoveShippingCompanyCommandRequest removeShippingCompanyCommandRequest)
		{
			RemoveShippingCompanyCommandResponse response = await _mediator.Send(removeShippingCompanyCommandRequest);
			return Ok(response);
		}

		[HttpPut("[action]")]
		public async Task<IActionResult> UpdateShippingCompany([FromBody] UpdateShippingCompanyCommandRequest updateShippingCompanyCommandRequest)
		{
			UpdateShippingCompanyCommandResponse response = await _mediator.Send(updateShippingCompanyCommandRequest);
			return Ok(response);
		}
	}
}
