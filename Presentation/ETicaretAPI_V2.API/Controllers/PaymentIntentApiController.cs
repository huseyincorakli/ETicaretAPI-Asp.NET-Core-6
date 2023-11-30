using System;
using System.Collections.Generic;
using Microsoft.AspNetCore;
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
	public class PaymentIntentApiController : ControllerBase
	{
		[HttpPost]
		public ActionResult Create([FromBody] PaymentIntentCreateRequest request)
		{
			var paymentIntentService = new PaymentIntentService();
			var paymentIntent = paymentIntentService.Create(new PaymentIntentCreateOptions
			{
				Amount = CalculateOrderAmount(request.Items),
				Currency = "try",
				AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
				{
					Enabled = true,
				},
			});

			return Ok(new { clientSecret = paymentIntent.ClientSecret });
		}
		private int CalculateOrderAmount(Item[] items)
		{
			int totalAmount=0;
			for (int i = 0; i < items.Length; i++)
			{
				totalAmount=totalAmount+ int.Parse(items[i].Amount);
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

