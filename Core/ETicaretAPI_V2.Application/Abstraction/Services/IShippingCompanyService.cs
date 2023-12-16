using ETicaretAPI_V2.Domain.Entities;

namespace ETicaretAPI_V2.Application.Abstraction.Services
{
	public interface IShippingCompanyService
	{
		Task<bool> AddCompanyAsync(ShippingCompany shippingCompany);
		Task<bool> RemoveCompanyAsync(string companyId);
		Task<List<ShippingCompany>> GetAll();
		Task<ShippingCompany> GetCompanyByIdAsync(string companyId);
		

	}
}
