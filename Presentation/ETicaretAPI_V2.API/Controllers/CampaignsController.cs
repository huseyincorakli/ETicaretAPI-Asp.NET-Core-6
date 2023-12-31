﻿using ETicaretAPI_V2.Application.Consts;
using ETicaretAPI_V2.Application.CustomAttributes;
using ETicaretAPI_V2.Application.Enums;
using ETicaretAPI_V2.Application.Features.Commands.Campaign.CreateCampaign;
using ETicaretAPI_V2.Application.Features.Commands.Campaign.DeleteCampaign;
using ETicaretAPI_V2.Application.Features.Commands.Campaign.UpdateShowcase;
using ETicaretAPI_V2.Application.Features.Queries.Campaign.GetActiveCampaign;
using ETicaretAPI_V2.Application.Features.Queries.Campaign.GetAllCampaign;
using ETicaretAPI_V2.Application.Features.Queries.Campaign.GetCampaignById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI_V2.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]

	public class CampaignsController : ControllerBase
	{
		readonly IMediator _mediator;

		public CampaignsController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost("[action]")]
		[Authorize(AuthenticationSchemes = "Admin")]
		[AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Campaigns, ActionType = ActionType.Writing, Definition = "Create Campaign")]
		public async Task<IActionResult> CreateCampaign([FromBody] CreateCampaignCommandRequest createCampaignCommandRequest)
		{
			CreateCampaignCommandResponse response = await _mediator.Send(createCampaignCommandRequest);
			return Ok(response);
		}

		[HttpDelete("[action]/{CampaignId}")]
		[Authorize(AuthenticationSchemes = "Admin")]
		[AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Campaigns, ActionType = ActionType.Deleting, Definition = "Delete Campaign")]
		public async Task<IActionResult> DeleteCampaign([FromRoute] DeleteCampaignCommandRequest deleteCampaignCommandRequest)
		{
			DeleteCampaignCommandResponse response = await _mediator.Send(deleteCampaignCommandRequest);
			return Ok(response);
		}

		[HttpPut("[action]")]
		[Authorize(AuthenticationSchemes = "Admin")]
		[AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Campaigns, ActionType = ActionType.Updating, Definition = "Update  Campaign Showcase")]
		public async Task<IActionResult> UpdateCampaingShowcase([FromBody] UpdateShowcaseCommandRequest updateShowcaseCommandRequest)
		{
			UpdateShowcaseCommandResponse response = await _mediator.Send(updateShowcaseCommandRequest);
			return Ok(response);
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> GetAllCampaign([FromQuery] GetAllCampaignQueryRequest getAllCampaignQueryRequest)
		{
			GetAllCampaignQueryResponse response = await _mediator.Send(getAllCampaignQueryRequest);


			return Ok(response);
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> GetActiveCampaign([FromQuery] GetActiveCampaignQueryRequest getActiveCampaignQueryRequest)
		{
			GetActiveCampaignQueryResponse response = await _mediator.Send(getActiveCampaignQueryRequest);


			return Ok(response);
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> GetCampaignById([FromQuery] GetCampaignByIdQueryRequest getCampaignByIdQueryRequest)
		{
			GetCampaignByIdQueryResponse response = await _mediator.Send(getCampaignByIdQueryRequest);


			return Ok(response);
		}
	}
}
