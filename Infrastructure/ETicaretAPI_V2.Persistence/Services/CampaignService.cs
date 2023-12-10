using ETicaretAPI_V2.Application.Abstraction.Services;
using ETicaretAPI_V2.Application.DTOs.Campaign;
using ETicaretAPI_V2.Application.Repositories.CampaignRepositories;
using ETicaretAPI_V2.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ETicaretAPI_V2.Persistence.Services
{
	public class CampaignService : ICampaignService
	{
		readonly ICampaignWriteRepository _campaignWriteRepository;
		readonly ICampaignReadRepository _campaignReadRepository;

		public CampaignService(ICampaignWriteRepository campaignWriteRepository, ICampaignReadRepository campaignReadRepository)
		{
			_campaignWriteRepository = campaignWriteRepository;
			_campaignReadRepository = campaignReadRepository;
		}

		public async Task<bool> CreatCampaignAsync(CreateCampaign createCampaign)
		{
			await _campaignWriteRepository.AddAsync(new()
			{
				Id = Guid.NewGuid(),
				Code = createCampaign.Code,
				Content = createCampaign.Content,
				ExpiredTime = createCampaign.ExpiredTime,
				ShowCase = createCampaign.Showcase,
				Title = createCampaign.Title,
			});
			var succes = await _campaignWriteRepository.SaveAsync();
			if (succes == 1)
			{
				return true;
			}
			else
				return false;

		}

		public async Task<bool> RemoveCampaingAsync(string campaingId)
		{
			Campaign campaign = await _campaignReadRepository.GetByIdAsync(campaingId);
			if (campaign != null)
			{
				 _campaignWriteRepository.Remove(campaign);
				int success = await _campaignWriteRepository.SaveAsync();
				if (success == 1)
				{
					return true;
				}
				else
					return false;
			}
			else
				return false;
		}

		public async Task<bool> UpdateCampaignAsync(CreateCampaign createCampaign, string campaingId)
		{
			Campaign? campaign = await _campaignReadRepository.GetByIdAsync(campaingId);
			if (campaign != null)
			{
				throw new Exception();
			}
			else
			{
				campaign.ShowCase = createCampaign.Showcase;
				campaign.Title = createCampaign.Title;
				campaign.Code= createCampaign.Code;
				campaign.Content = createCampaign.Content;
				campaign.ExpiredTime=campaign.ExpiredTime;

				int success = await _campaignWriteRepository.SaveAsync();
				if (success == 1)
				{
					return true;
				}
				else
					return false;
			}
		}

		public async Task<bool> UpdateShowcaseAsync(string showcaseId,bool showValue)
		{
			if (showValue==true)
			{
				var datas = await _campaignReadRepository.GetAll().ToListAsync();
				foreach (var item in datas)
				{
					item.ShowCase = false;
					await _campaignWriteRepository.SaveAsync();
				}
				var data = await _campaignReadRepository.GetByIdAsync(showcaseId);
				data.ShowCase = true;
				var response= await _campaignWriteRepository.SaveAsync();
				if (response == 1)
				{
					return true;
				}
				else
					return false;
			}
			else
			{
				var data = await _campaignReadRepository.GetByIdAsync(showcaseId);
				data.ShowCase = showValue;
				var response = await _campaignWriteRepository.SaveAsync();
				if (response == 1)
				{
					return true;
				}
				else
					return false;
			}

		}
	}
}
