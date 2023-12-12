using ETicaretAPI_V2.Application.DTOs.Campaign;
using ETicaretAPI_V2.Domain.Entities;

namespace ETicaretAPI_V2.Application.Abstraction.Services
{
	public interface ICampaignService
	{
		Task<bool> CreatCampaignAsync(CreateCampaign createCampaign);
		Task<bool> RemoveCampaingAsync(string campaingId);
		Task<bool> UpdateCampaignAsync(CreateCampaign createCampaign, string campaingId);
		Task<bool> UpdateShowcaseAsync(string showcaseId, bool showValue);
		Task<List<Campaign>> GetAllCampaignAsync(int size, string campaignCode);
		Task<Campaign> GetCampaignByIdAsync(string campaignId);
		Task<Campaign> GetActiveCampaign();

	}
}
