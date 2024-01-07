using ETicaretAPI_V2.Application.Abstraction.Services;
using ETicaretAPI_V2.Application.Consts;
using ETicaretAPI_V2.Application.Repositories.RefundRepositories;
using ETicaretAPI_V2.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ETicaretAPI_V2.Persistence.Services
{
	public class RefundService : IRefundService
	{
		readonly IRefundReadRepository _refundReadRepository;
		readonly IRefundWriteRepository _refundWriteRepository;

		public RefundService(IRefundReadRepository refundReadRepository, IRefundWriteRepository refundWriteRepository)
		{
			_refundReadRepository = refundReadRepository;
			_refundWriteRepository = refundWriteRepository;
		}

		public async Task<bool> CreateRefundAsync(Refund refund)
		{
			var data = await _refundWriteRepository.AddAsync(refund);
			await SaveAsync();
			return data;
		}

		public async Task<bool> DeleteRefundAsync(string refundId)
		{
			Refund refund = await _refundReadRepository.GetByIdAsync(refundId);
			_refundWriteRepository.Remove(refund);
			await SaveAsync();
			if (refund != null)
			{
				return true;
			}
			else
				return false;

		}

		public async Task<bool> CheckRefundForOrder(string orderCode,string email)
		{
			var data = await _refundReadRepository.GetAll().FirstOrDefaultAsync(p => p.OrderCode == orderCode && p.Email == email);
			if (data!=null)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public async Task<List<Refund>> GetAllRefundsAsync(int size)
		{
			List<Refund> refunds = await _refundReadRepository.Table.Take(size).ToListAsync();
			if(refunds.Count > 0)
			{
				return refunds;

			}
			else
			{
				return null;
			}
		}

		public async Task<List<Refund>> GetRefundsByEmail(string email,int size)
		{
			List<Refund> refunds =  await _refundReadRepository.GetWhere(x=>x.Email==email).Take(size).ToListAsync();
			if (refunds.Count > 0)
			{
				return refunds;
			}
			else
				return null;
		}
		public async Task<bool> ChangeStatus(string refundId,int value)
		{
		Refund  refund =	await _refundReadRepository.GetByIdAsync(refundId);
			if (value==1)
			{
				refund.ReturnStatus = RefundReturnStatu.Accepted;
				await SaveAsync();
				return true;
			}
			if (value==0)
			{
				refund.ReturnStatus = RefundReturnStatu.Denied;
				await SaveAsync();
				return true;

			}
			else
			{
				return false;
			}
		}
		public async Task SaveAsync()
		{
			await _refundWriteRepository.SaveAsync();
		}
	}
}
