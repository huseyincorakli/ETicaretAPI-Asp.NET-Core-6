using ETicaretAPI_V2.Application.Abstraction.Services;
using ETicaretAPI_V2.Application.Repositories.ShippingCompanyRepositories;
using ETicaretAPI_V2.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ETicaretAPI_V2.Persistence.Services
{
	public class ShippingCompanyService : IShippingCompanyService
	{
		readonly IShippingCompanyReadRepository _shippingCompanyReadRepository;
		readonly IShippingCompanyWriteRepository _shippingCompanyWriteRepository;

		public ShippingCompanyService(IShippingCompanyReadRepository shippingCompanyReadRepository, IShippingCompanyWriteRepository shippingCompanyWriteRepository)
		{
			_shippingCompanyReadRepository = shippingCompanyReadRepository;
			_shippingCompanyWriteRepository = shippingCompanyWriteRepository;
		}

		public async Task<bool> AddCompanyAsync(ShippingCompany shippingCompany)
		{
			bool data = await _shippingCompanyWriteRepository.AddAsync(shippingCompany);

			await _shippingCompanyWriteRepository.SaveAsync();
			if (data)
			{
				return true;
			}
			else
				return false;

		}

		public async Task<List<ShippingCompany>> GetAll()
		{
			return await _shippingCompanyReadRepository.GetAll().ToListAsync();
		}

		public async Task<ShippingCompany> GetCompanyByIdAsync(string companyId)
		{
			return await _shippingCompanyReadRepository.GetByIdAsync(companyId);
		}

		public async Task<bool> RemoveCompanyAsync(string companyId)
		{
			bool data = await _shippingCompanyWriteRepository.RemoveAsync(companyId);
			await _shippingCompanyWriteRepository.SaveAsync();
			if (data)
			{
				return true;
			}
			else
				return false;
		}
	}
}
