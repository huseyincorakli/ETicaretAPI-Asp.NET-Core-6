﻿using ETicaretAPI_V2.Application.Features.Commands.Category.ChangeCategoryStatus;
using ETicaretAPI_V2.Application.Features.Commands.Category.CreateCategory;
using ETicaretAPI_V2.Application.Features.Commands.Category.UpdateCategory;
using ETicaretAPI_V2.Application.Features.Queries.Category.GetAllCategory;
using ETicaretAPI_V2.Application.Features.Queries.Category.GetCategoryById;
using MediatR;
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

		[HttpGet("{CategoryId}")]
		public async Task<IActionResult> GetCategoryById([FromRoute] GetCategoryByIdQueryRequest getCategoryByIdQueryRequest)
		{
			var response= await _mediator.Send(getCategoryByIdQueryRequest);

			return Ok(response);
		}

		[HttpPost("[action]")]
		public async Task<IActionResult> CreateCategory([FromBody]CreateCategoryCommandRequest createCategoryCommandRequest)
		{
			var response= await _mediator.Send(createCategoryCommandRequest);
			return Ok(response);
		}

		[HttpPut("[action]")]
		public async Task<IActionResult> UpdateCategory([FromBody]UpdateCategoryCommandRequest updateCategoryCommandRequest)
		{
			var response = await _mediator.Send(updateCategoryCommandRequest);
			return Ok(response);
		}

		[HttpPut("[action]")]
		public async Task<IActionResult> ChangeCategoryStatus(ChangeCategoryStatusCommandRequest changeCategoryStatusCommandRequest)
		{
			var response=  await _mediator.Send(changeCategoryStatusCommandRequest);
			return Ok(response);
		}
	}
}
