using ETicaretAPI_V2.Application.Abstraction.Services;
using ETicaretAPI_V2.Application.DTOs.Address;
using ETicaretAPI_V2.Application.Repositories.AddressRepositories;
using ETicaretAPI_V2.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ETicaretAPI_V2.Persistence.Services
{
	public class AddressService : IAddressService
	{
		readonly IAddressReadRepository _addressReadRepository;
		readonly IAddressWriteRepository _addressWriteRepository;
		readonly IUserService _userService;
		public AddressService(IAddressReadRepository addressReadRepository, IAddressWriteRepository addressWriteRepository, IUserService userService)
		{
			_addressReadRepository = addressReadRepository;
			_addressWriteRepository = addressWriteRepository;
			_userService = userService;
		}
		public async Task SaveAsync()
		{
			await _addressWriteRepository.SaveAsync();
		}
		public async Task<bool> AddAdressAsync(CreateAddress address)
		{
		 var user =	await _userService.GetUserById(address.UserId);
			if (user!=null)
			{
				if (user.Address!=null)
				{
					throw new Exception("User has the address");
				}
				else
				{
					var result = await _addressWriteRepository.AddAsync(new()
					{
						Id = Guid.NewGuid(),
						AddressInfo = address.AddressInfo,
						UserId = address.UserId,
						City = address.City,
						County = address.County,
						CreateDate = DateTime.UtcNow,
						Directions = address.Directions,
						NameSurname = address.NameSurname,
						TelNumber = address.TelNumber,
						UpdatedDate = DateTime.UtcNow,
					});
					await SaveAsync();
					return result;
				}
			}
			throw new Exception("User not founded!");
			 
		}

		public async Task<Address> GetAddressAsync(string addressId)
		{
			Address address = await _addressReadRepository.GetByIdAsync(addressId);
			if (address != null)
				return address;
			else
				return null;
		}

		public async Task<Address> GetAddressByUserIDAsync(string userId)
		{
			var data =  await _addressReadRepository.Table.Where(a=>a.UserId==userId).SingleOrDefaultAsync();
			return data;
		}

		public async Task<bool> RemoveAdressAsync(string addressId)
		{
			Address address = await _addressReadRepository.GetByIdAsync(addressId);
			if (address!=null)
			{
				_addressWriteRepository.Remove(address);
				await SaveAsync();
				return true;
			}
			else
			{
				return false;
			}
		}

		public async Task<bool> UpdateAdressAsync(string addressId , UpdateAddress updateAddress)
		{
			Address address =	await _addressReadRepository.GetByIdAsync(addressId);
			if (address != null)
			{
				address.NameSurname= updateAddress.NameSurname;
				address.TelNumber = updateAddress.TelNumber;
				address.UpdatedDate = DateTime.UtcNow;
				address.AddressInfo = updateAddress.AddressInfo;
				address.City = updateAddress.City;
				address.Directions = updateAddress.Directions;
				address.County = updateAddress.County;
				await SaveAsync();
				return true;
			}
			else
				return false;
		}
	}
}
