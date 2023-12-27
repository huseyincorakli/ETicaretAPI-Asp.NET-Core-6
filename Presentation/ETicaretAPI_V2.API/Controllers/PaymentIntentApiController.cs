using System;
using System.Collections.Generic;
using ETicaretAPI_V2.Application.Consts;
using ETicaretAPI_V2.Application.CustomAttributes;
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


		public PaymentIntentApiController(ICampaignUsageWriteRepository campaignUsageWriteRepository, ICampaignUsageReadRepository campaignUsageReadRepository, ICampaignReadRepository campaignReadRepository)
		{
			_campaignUsageWriteRepository = campaignUsageWriteRepository;
			_campaignUsageReadRepository = campaignUsageReadRepository;
			_campaignReadRepository = campaignReadRepository;
		}

		[HttpPost]
		[AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Payment, ActionType = ActionType.Writing, Definition = "Create Payment")]
		public ActionResult Create([FromBody] PaymentIntentCreateRequest request)
		{
			var paymentIntentService = new PaymentIntentService();
			var paymentIntent = paymentIntentService.Create(new PaymentIntentCreateOptions
			{
				Amount = CalculateOrderAmount(request.Items)*100,
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

			///kullanıcı bilgilerine erişmek için //paymentMethodId ile
			//var service = new PaymentMethodService();
			//var xyz= service.Get("pm_1ORIlEFTVXhGXu7OT9X83OjD");
			//return Ok(xyz);

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

		private int CalculateOrderAmount(Item[] items)
		{
			int totalAmount=0;
			for (int i = 0; i < items.Length; i++)
			{
				totalAmount=totalAmount+ int.Parse(items[i].Amount.Split('.')[0]);
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

