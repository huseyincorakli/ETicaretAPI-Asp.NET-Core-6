using System;
using System.Collections.Generic;
using ETicaretAPI_V2.Application.Abstraction.Services;
using ETicaretAPI_V2.Application.Consts;
using ETicaretAPI_V2.Application.CustomAttributes;
using ETicaretAPI_V2.Application.DTOs.Refund;
using ETicaretAPI_V2.Application.Enums;
using ETicaretAPI_V2.Application.Repositories.CampaignRepositories;
using ETicaretAPI_V2.Application.Repositories.CampaignUsageRepositories;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Stripe;
namespace ETicaretAPI_V2.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	//[Authorize(AuthenticationSchemes = "Admin")]
	public class PaymentIntentApiController : ControllerBase
	{
		readonly ICampaignUsageWriteRepository _campaignUsageWriteRepository;
		readonly ICampaignUsageReadRepository _campaignUsageReadRepository;
		readonly ICampaignReadRepository _campaignReadRepository;
		readonly IRefundService _refundService;
		readonly IMailService _mailService;
		readonly IUserService _userService;
		readonly IOrderService _orderService;



		public PaymentIntentApiController(ICampaignUsageWriteRepository campaignUsageWriteRepository, ICampaignUsageReadRepository campaignUsageReadRepository, ICampaignReadRepository campaignReadRepository, IRefundService refundService, IMailService mailService, IUserService userService, IOrderService orderService)
		{
			_campaignUsageWriteRepository = campaignUsageWriteRepository;
			_campaignUsageReadRepository = campaignUsageReadRepository;
			_campaignReadRepository = campaignReadRepository;
			_refundService = refundService;
			_mailService = mailService;
			_userService = userService;
			_orderService = orderService;
		}

		[HttpPost]
		[AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Payment, ActionType = ActionType.Writing, Definition = "Create Payment")]
		public ActionResult Create([FromBody] PaymentIntentCreateRequest request)
		{
			var paymentIntentService = new PaymentIntentService();
			var paymentIntent = paymentIntentService.Create(new PaymentIntentCreateOptions
			{
				Amount = CalculateOrderAmount(request.Items) * 100,
				Currency = "try",
				AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
				{
					Enabled = true,
				},
					
			});


			return Ok(new { clientSecret = paymentIntent.ClientSecret });
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> GetPayments(long limit)
		{
			PaymentIntentListOptions abc = new()
			{
				Limit = limit,

			};
			var paymentIntentService = new PaymentIntentService();
			var payments = await paymentIntentService.ListAsync(abc);

			return Ok(payments);
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> GetPaymentDetail(string PaymentMethodId)
		{
			var service = new PaymentMethodService();
			var detail = service.Get(PaymentMethodId);
			return Ok(detail);
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> GetBalances()
		{
			var service = new BalanceService();
			var data = service.Get();
			return Ok(data);
		}

		[HttpGet("[action]")]
		[AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Payment, ActionType = ActionType.Reading, Definition = "Set Campaign Usage")]
		public async Task<IActionResult> SetCampaignUsage([FromQuery] string userId, string campaignId)
		{
			var data = await _campaignUsageReadRepository.GetSingleAsync(s => s.UserId == userId && s.CampaignId == Guid.Parse(campaignId));
			if (data == null)
			{
				await _campaignUsageWriteRepository.AddAsync(new()
				{
					CampaignId = Guid.Parse(campaignId),
					UserId = userId,
					UsageTime = DateTime.UtcNow,
					Id = Guid.NewGuid()
				});
				await _campaignUsageWriteRepository.SaveAsync();
			}
			else
			{
				throw new Exception("KULLANILMIŞ");
			}
			return Ok();
		}

		[HttpGet("[action]")]

		public async Task<IActionResult> GetCampaignIdByCode(string code)
		{
			var data = await _campaignReadRepository.GetSingleAsync(u => u.Code == code);

			return Ok(data.Id);

		}

		[HttpPost("[action]")]
		public async Task<IActionResult> CreateRefund(CreateRefund refund)
		{
			var result = await _refundService.CheckRefundForOrder(refund.OrderCode, refund.Email);
			if (result!=false)
			{
				var data = await _refundService.CreateRefundAsync(new()
				{
					Id = Guid.NewGuid(),
					Email = refund.Email,
					Name = refund.Name,
					OrderCode = refund.OrderCode,
					Reason = refund.Reason,
					ReturnStatus = RefundReturnStatu.Examining
				});

				return Ok(data);
			}
			else
			{
				throw new Exception("Bu sipariş için iade talebi oluşturulmuş");
			}
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> GetAllRefunds([FromQuery]int size)
		{
		 var data= 	await _refundService.GetAllRefundsAsync(size);
			return Ok(data);
		}
		
		[HttpGet("[action]")]
		public async Task<IActionResult> GetRefundsByEmail(string email, int size)
		{
			 var data = await _refundService.GetRefundsByEmail(email,size);
			return Ok(data);
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> UpdateRefundStatus( string refundId,int value)
		{
			var data = await _refundService.ChangeStatus(refundId, value);
			return Ok(data);
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> GetPaymentsByMail(string email)
		{
			var paymentIntentService = new PaymentIntentService();
			var paymentIntents = await paymentIntentService.ListAsync();

			var service = new PaymentMethodService();
			var detailsList = new List<PaymentMethod>();

			foreach (var item in paymentIntents)
			{
				var detail = await service.GetAsync(item.PaymentMethodId);
				detailsList.Add(detail);

				item.PaymentMethod = detail; 
			}

			var payments = new List<PaymentIntent>();
			foreach (var item in paymentIntents)
			{
				if (item.PaymentMethod.BillingDetails.Email == $"{email}")
				{
					payments.Add(item);
				}
			}

			return Ok(payments);

		}

		[HttpGet("[action]")]
		public async Task<IActionResult> RefundAccept(string paymentIntentId,long amount,string message,string userId,string orderCode)
		{
			var options = new RefundCreateOptions
			{
				PaymentIntent = paymentIntentId,
				Amount = amount*100,
			};
			var service = new RefundService();
 			var data = await service.CreateAsync(options);
			var result = Ok(data.Status);

			if (result.StatusCode==200)
			{
				await AgreeRefundMessage(userId, message);
				await _orderService.RemoveOrderByOrderCode(orderCode,userId);
			}

			return result;
		}
		[HttpGet("[action]")]
		public async Task<IActionResult> RefundReject(string message, string userId)
		{
			await RejectRefundMessage(userId,message);
			return Ok();
		}
		private async Task AgreeRefundMessage(string userId, string message)
		{
			var user = await _userService.GetUserById(userId);
			await _mailService.SendMailAsync(user.Email, "Sipariş İadesi Kabul Edildi", message);
		}

		private async Task RejectRefundMessage(string userId, string message)
		{
			var user = await _userService.GetUserById(userId);
			await _mailService.SendMailAsync(user.Email, "Sipariş İadesi Reddedildi", message);
		}

		private int CalculateOrderAmount(Item[] items)
		{
			int totalAmount = 0;
			for (int i = 0; i < items.Length; i++)
			{
				totalAmount = totalAmount + int.Parse(items[i].Amount.Split('.')[0]);
			}
			return totalAmount;
		}
		public class Item
		{
			public string Id { get; set; }
			public string Amount { get; set; }
		}

		public class PaymentIntentCreateRequest
		{
			public Item[] Items { get; set; }
		}

	}
}

