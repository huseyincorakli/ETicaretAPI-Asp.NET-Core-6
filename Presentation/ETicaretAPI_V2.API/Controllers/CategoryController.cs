using ETicaretAPI_V2.Application.Consts;
using ETicaretAPI_V2.Application.CustomAttributes;
using ETicaretAPI_V2.Application.Enums;
using ETicaretAPI_V2.Application.Features.Commands.Category.ChangeCategoryStatus;
using ETicaretAPI_V2.Application.Features.Commands.Category.CreateCategory;
using ETicaretAPI_V2.Application.Features.Commands.Category.GetAllCategoryName;
using ETicaretAPI_V2.Application.Features.Commands.Category.UpdateCategory;
using ETicaretAPI_V2.Application.Features.Queries.Category.GetAllCategory;
using ETicaretAPI_V2.Application.Features.Queries.Category.GetCategoryById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI_V2.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		readonly IMediator _mediator;

		public CategoryController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> GetAllCategories([FromQuery]GetAllCategoryQueryRequest getAllCategoryQueryRequest)
		{
			var response = await _mediator.Send(getAllCategoryQueryRequest);
			return Ok(response);
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> GetAllCategoryName()
		{
			var response = await _mediator.Send(new GetAllCategoryNameCommandRequest());
			return Ok(response);
		}

		[HttpGet("{CategoryId}")]
		public async Task<IActionResult> GetCategoryById([FromRoute] GetCategoryByIdQueryRequest getCategoryByIdQueryRequest)
		{
			var response= await _mediator.Send(getCategoryByIdQueryRequest);

			return Ok(response);
		}

		[HttpPost("[action]")]
		[Authorize(AuthenticationSchemes = "Admin")]
		[AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Category, ActionType = ActionType.Writing, Definition = "Create Category")]
		public async Task<IActionResult> CreateCategory([FromBody]CreateCategoryCommandRequest createCategoryCommandRequest)
		{
			var response= await _mediator.Send(createCategoryCommandRequest);
			return Ok(response);
		}

		[HttpPut("[action]")]
		[Authorize(AuthenticationSchemes = "Admin")]
		[AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Category, ActionType = ActionType.Updating, Definition = "Update Category")]
		public async Task<IActionResult> UpdateCategory([FromBody]UpdateCategoryCommandRequest updateCategoryCommandRequest)
		{
			var response = await _mediator.Send(updateCategoryCommandRequest);
			return Ok(response);
		}

		[HttpPut("[action]")]
		[Authorize(AuthenticationSchemes = "Admin")]
		[AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Category, ActionType = ActionType.Updating, Definition = "Change Category Status")]
		public async Task<IActionResult> ChangeCategoryStatus([FromBody]ChangeCategoryStatusCommandRequest changeCategoryStatusCommandRequest)
		{
			var response=  await _mediator.Send(changeCategoryStatusCommandRequest);
			return Ok(response);
		}

		
	}
}
