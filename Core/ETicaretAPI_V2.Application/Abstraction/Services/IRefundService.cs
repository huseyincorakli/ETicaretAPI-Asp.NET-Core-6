using ETicaretAPI_V2.Domain.Entities;

namespace ETicaretAPI_V2.Application.Abstraction.Services
{
	public interface IRefundService
	{
		Task<bool> CreateRefundAsync(Refund refund);
		Task SaveAsync();
		Task<bool> ChangeStatus(string refundId,int value);
		Task<bool> DeleteRefundAsync(string refundId);
		Task<List<Refund>> GetAllRefundsAsync(int size);
		Task<List<Refund>> GetRefundsByEmail(string email,int size);
		Task<bool> CheckRefundForOrder(string orderCode, string email);
	}
}
