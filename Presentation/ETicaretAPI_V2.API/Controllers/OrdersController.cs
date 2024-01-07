using ETicaretAPI_V2.Application.Abstraction.Services;
using ETicaretAPI_V2.Application.Consts;
using ETicaretAPI_V2.Application.CustomAttributes;
using ETicaretAPI_V2.Application.Enums;
using ETicaretAPI_V2.Application.Features.Commands.Order.CompleteOrder;
using ETicaretAPI_V2.Application.Features.Commands.Order.CreateOrder;
using ETicaretAPI_V2.Application.Features.Queries.Order.GetAllOrder;
using ETicaretAPI_V2.Application.Features.Queries.Order.GetDailySale;
using ETicaretAPI_V2.Application.Features.Queries.Order.GetOrderById;
using ETicaretAPI_V2.Application.Features.Queries.Order.GetUnCompletedOrders;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI_V2.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(AuthenticationSchemes ="Admin")]
	public class OrdersController : ControllerBase
	{
		readonly IMediator _mediator;
		readonly IOrderService _orderService;


		public OrdersController(IMediator mediator, IOrderService orderService)
		{
			_mediator = mediator;
			_orderService = orderService;
		}

		[HttpGet("[action]")]
		[AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Orders, ActionType = ActionType.Reading, Definition = "GetUnCompletedOrders")]

		public async Task<IActionResult> GetUnCompletedOrders([FromQuery] GetUnCompletedOrdersQueryRequest getUnCompletedOrdersQueryRequest)
		{
			GetUnCompletedOrdersQueryResponse response = await _mediator.Send(getUnCompletedOrdersQueryRequest);
			return Ok(response);
		}

		[HttpPost]
		[AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Orders, ActionType = ActionType.Writing, Definition = "Create Order")]
		public async Task<IActionResult> CreateOrder(CreateOrderCommandRequest createOrderCommandRequest)
		{
			CreateOrderCommandResponse response = await _mediator.Send(createOrderCommandRequest);
			return Ok(response);
		}

		[HttpGet]
		[AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Orders, ActionType = ActionType.Reading, Definition = "Get All Orders")]
		public async Task<IActionResult> GetAllOrders([FromQuery] GetAllOrdersQueryRequest getAllOrdersQueryRequest)
		{

			GetAllOrdersQueryResponse response = await _mediator.Send(getAllOrdersQueryRequest);
			return Ok(response);
		}

		[HttpGet("{Id}")]
		[AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Orders, ActionType = ActionType.Reading, Definition = "Get Order By Id")]
		public async Task<IActionResult> GetOrderById([FromRoute] GetOrderByIdQueryRequest getOrderByIdQueryRequest)
		{
			GetOrderByIdQueryResponse response = await _mediator.Send(getOrderByIdQueryRequest);

			return Ok(response);
		}

		[HttpPost("[action]")]
		[AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Orders, ActionType = ActionType.Updating, Definition = "Complete Order")]
		public async Task<IActionResult> CompleteOrder([FromBody] CompleteOrderCommandRequest completeOrderCommandRequest)
		{
			CompleteOrderCommandResponse response = await _mediator.Send(completeOrderCommandRequest);
			return Ok(response);
		}

		[HttpGet("[action]")]
		[AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Orders, ActionType = ActionType.Reading, Definition = "GetDailySale")]

		public async Task<IActionResult> GetDailySale([FromQuery] GetDailySaleQueryRequest getDailySaleQueryRequest)
		{
			GetDailySaleQueryResponse response = await _mediator.Send(getDailySaleQueryRequest);
			return Ok(response);
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> GetOrdersByUserId([FromQuery] string userId, int size)
		{
			var data = await _orderService.GetOrderByUserId(size, userId);
			if(data!=null)
				return Ok(data);
			else
				return Ok(null);
		}
	}
}
