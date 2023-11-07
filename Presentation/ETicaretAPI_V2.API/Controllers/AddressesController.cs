using ETicaretAPI_V2.Application.DTOs.Address;
using ETicaretAPI_V2.Application.Features.Commands.Address.AddAddress;
using ETicaretAPI_V2.Application.Features.Commands.Address.RemoveAddress;
using ETicaretAPI_V2.Application.Features.Commands.Address.UpdateAddress;
using ETicaretAPI_V2.Application.Features.Queries.Address.GetAddressById;
using ETicaretAPI_V2.Application.Features.Queries.Address.GetAddressByUserId;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI_V2.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AddressesController : ControllerBase
	{
		readonly IMediator _mediator;

		public AddressesController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost]
		public async Task<IActionResult> Post(CreateAddressCommandRequest createAddressCommandRequest)
		{
			var response = await _mediator.Send(createAddressCommandRequest);
			return Ok(response);
		}

		[HttpGet("[action]/{AddressId}")]
		public async Task<IActionResult> GetAddressById([FromRoute] GetAddressByIdQueryRequest getAddressByIdQueryRequest)
		{
			var data = await _mediator.Send(getAddressByIdQueryRequest);
			return Ok(data) ;
		}

		[HttpGet("[action]/{UserId}")]
		public	async Task<IActionResult> GetAddressByUserId([FromRoute]GetAddressByUserIdQueryRequest getAddressByUserIdQueryRequest)
		{
			var data = await _mediator.Send(getAddressByUserIdQueryRequest);
			return Ok(data);
		}

		[HttpDelete("[action]/{AddressId}")]
		public async Task<IActionResult> RemoveAddress([FromRoute] RemoveAddressCommandRequest removeAddressCommandRequest)
		{
			var response = await _mediator.Send(removeAddressCommandRequest);
			return Ok(response);
		}
		[HttpPut("[action]")]
		public async Task<IActionResult> UpdateAddress([FromBody] UpdateAddressCommandRequest updateAddressCommandRequest)
		{
			var response = await _mediator.Send(updateAddressCommandRequest);
			return Ok(response);
		}

		
	}
}
