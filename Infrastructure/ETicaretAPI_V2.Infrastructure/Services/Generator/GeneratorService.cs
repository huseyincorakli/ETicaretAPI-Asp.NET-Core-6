using ETicaretAPI_V2.Application.Abstraction.Services;
using ETicaretAPI_V2.Application.DTOs.ProductDesciription;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ETicaretAPI_V2.Infrastructure.Services.Generator
{
	public class GeneratorService : IGeneratorService
	{


		public async Task<string> ProductDescriptionGenerator(string brand, string category, string productDesciription, string[] keywords, string name)
		{

			var client = new HttpClient();
			var contentJson = new
			{
				brand,
				category,
				description = productDesciription,
				keywords,
				max_tokens = 250,
				model = "chat-sophos-1",
				n = 1,
				name,
				source_lang = "tr",
				target_lang = "tr",
				temperature = 0.35
			};

			var content = new StringContent(JsonConvert.SerializeObject(contentJson))
			{
				Headers =
				{
					ContentType = new MediaTypeHeaderValue("application/json")
				}
			};
			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Post,
				RequestUri = new Uri("https://api.textcortex.com/v1/texts/products/descriptions"),
				Headers =
					{
						{ "Authorization", "Bearer gAAAAABlNQDefyk0uhbo0KPsZnltVCMLC_8CpD6QKrZkh3alaFi8FTd3QwXYGRfaypbQ39YhxSmrMrOPL8zzwsztZnWCFdUogT1dZJUYCpBGsGbhxKVyFgf9xgqnYlAbPNiKcZvPzrdW" },
					},
				Content = content
			};

			var response = await client.SendAsync(request);
			response.EnsureSuccessStatusCode();
			var body = await response.Content.ReadAsStringAsync();
			Description? result = JsonConvert.DeserializeObject<Description>(body);
			if (result != null)
			{
				return result.Data.Outputs[0].Text;
			}
			else
			{
				return null;
			}
		}
	}
}


