using ETicaretAPI_V2.Application.Consts;
using ETicaretAPI_V2.Application.CustomAttributes;
using ETicaretAPI_V2.Application.Enums;
using ETicaretAPI_V2.Application.Features.Commands.ShippingCompany.CreateCompany;
using ETicaretAPI_V2.Application.Features.Commands.ShippingCompany.RemoveCompany;
using ETicaretAPI_V2.Application.Features.Commands.ShippingCompany.UpdateCompany;
using ETicaretAPI_V2.Application.Features.Queries.ShippingCompany.cs.GetAll;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI_V2.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(AuthenticationSchemes = "Admin")]
	public class ShippingCompanyController : ControllerBase
	{
		readonly IMediator _mediator;

		public ShippingCompanyController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet("[action]")]
		[AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Shipping, ActionType = ActionType.Reading, Definition = "Get All Shipping Company")]
		public async Task<IActionResult> GetAllShippingCompanies([FromQuery]GetAllShippingCompanyQueryRequest getAllShippingCompanyQueryRequest)
		{
			GetAllShippingCompanyQueryResponse response = await _mediator.Send(getAllShippingCompanyQueryRequest);
			return Ok(response);
		}

		[HttpPost]
		[AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Shipping, ActionType = ActionType.Writing, Definition = "Create Shipping Company")]
		public async Task<IActionResult> CreateShippingCompany([FromBody] CreateShippingCompanyCommandRequest createShippingCompanyCommandRequest)
		{
			CreateShippingCompanyCommandResponse response = await _mediator.Send(createShippingCompanyCommandRequest);
			return Ok(response);
		}

		[HttpDelete("[action]")]
		[AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Shipping, ActionType = ActionType.Deleting, Definition = "Remove Shipping Company")]
		public async Task<IActionResult> RemoveShippingCompany([FromQuery] RemoveShippingCompanyCommandRequest removeShippingCompanyCommandRequest)
		{
			RemoveShippingCompanyCommandResponse response = await _mediator.Send(removeShippingCompanyCommandRequest);
			return Ok(response);
		}

		[HttpPut("[action]")]
		[AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Shipping, ActionType = ActionType.Updating, Definition = "Update Shipping Company")]
		public async Task<IActionResult> UpdateShippingCompany([FromBody] UpdateShippingCompanyCommandRequest updateShippingCompanyCommandRequest)
		{
			UpdateShippingCompanyCommandResponse response = await _mediator.Send(updateShippingCompanyCommandRequest);
			return Ok(response);
		}
	}
}
