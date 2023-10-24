using ETicaretAPI_V2.Application.DTOs.ProductDesciription;

namespace ETicaretAPI_V2.Application.Abstraction.Services
{
	public interface IGeneratorService
	{
		Task<string> ProductDescriptionGenerator(string brand,string category,string productDesciription, string[]keywords,string name );
	}
}

