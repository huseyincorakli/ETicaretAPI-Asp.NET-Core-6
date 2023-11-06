using ETicaretAPI_V2.Application.DTOs.Address;
using ETicaretAPI_V2.Domain.Entities;

namespace ETicaretAPI_V2.Application.Abstraction.Services
{
	public interface IAddressService
	{
		public Task<bool> AddAdressAsync( CreateAddress address);
		public Task<bool> RemoveAdressAsync( string addressId);
		public Task<bool> UpdateAdressAsync( string addressId, UpdateAddress updateAddress);
		public Task<Address> GetAddressAsync(string addressId);
		public Task<Address>GetAddressByUserIDAsync(string userId);
		public Task SaveAsync();
	}
}
