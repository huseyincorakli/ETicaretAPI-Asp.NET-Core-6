using ETicaretAPI_V2.Application.DTOs.Campaign;

namespace ETicaretAPI_V2.Application.Abstraction.Services
{
	public interface ICampaignService
	{
		Task<bool> CreatCampaignAsync(CreateCampaign createCampaign);
		Task<bool> RemoveCampaingAsync(string campaingId);
		Task<bool> UpdateCampaignAsync(CreateCampaign createCampaign, string campaingId);
		Task<bool> UpdateShowcaseAsync(string showcaseId, bool showValue);

	}
}
