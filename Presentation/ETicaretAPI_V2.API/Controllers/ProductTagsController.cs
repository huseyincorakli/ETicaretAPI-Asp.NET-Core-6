using ETicaretAPI_V2.Application.Features.Commands.Category.CreateCategory;
using ETicaretAPI_V2.Application.Features.Commands.Product.UpdateProduct;
using ETicaretAPI_V2.Application.Features.Commands.ProductTag.ChangeProductTagStatus;
using ETicaretAPI_V2.Application.Features.Commands.ProductTag.CreateProductTag;
using ETicaretAPI_V2.Application.Features.Commands.ProductTag.UpdateProductTag;
using ETicaretAPI_V2.Application.Features.Queries.ProductTag.GetAllProductTag;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI_V2.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductTagsController : ControllerBase
	{
		readonly IMediator _mediator;

		public ProductTagsController(IMediator mediator)
		{
			_mediator = mediator;
		}
		[HttpGet]
		public async Task<IActionResult> GetAllProductTags([FromQuery] GetAllProductTagQueryRequest getAllProductTagQueryRequest)
		{
			var response = await _mediator.Send(getAllProductTagQueryRequest);
			return Ok(response);
		}

		[HttpPost]
		public async Task<IActionResult> Post( CreateProductTagCommandRequest createProductTagCommandRequest)
		{
			var response = await _mediator.Send(createProductTagCommandRequest);
			return Ok(response);
		}

		[HttpPut("[action]")]
		public async Task<IActionResult> UpdateProductTag([FromBody] UpdateProductTagCommandRequest updateProductTagCommandRequest)
		{
			var response= await _mediator.Send(updateProductTagCommandRequest);
			return Ok(response);
		}

		[HttpPut("[action]")]
		public async Task<IActionResult> ChangeProductTagStatus([FromBody] ChangeProductTagStatusCommandRequest changeProductTagStatusCommandRequest)
		{
			var response= await _mediator.Send(changeProductTagStatusCommandRequest);
			return Ok(response);
		}
	}
}
