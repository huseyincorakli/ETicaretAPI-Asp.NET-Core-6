using ETicaretAPI_V2.Application.Abstraction.Storage;
using ETicaretAPI_V2.Application.Repositories.HomeSettingRepositories;
using ETicaretAPI_V2.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace ETicaretAPI_V2.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class HomeSettingsController : ControllerBase
	{
		IStorageService _storageService;
		readonly IWebHostEnvironment _webHostEnvironment;
		readonly IHomeSettingReadRepositories _homeSettingReadRepositories;
		readonly IHomeSettingWriteRepositories _homeSettingWriteRepositories;

		public HomeSettingsController(IStorageService storageService, IWebHostEnvironment webHostEnvironment, IHomeSettingReadRepositories homeSettingReadRepositories, IHomeSettingWriteRepositories homeSettingWriteRepositories)
		{
			_storageService = storageService;
			_webHostEnvironment = webHostEnvironment;
			_homeSettingReadRepositories = homeSettingReadRepositories;
			_homeSettingWriteRepositories = homeSettingWriteRepositories;
		}

		[HttpPost("[action]")]
		public async Task<IActionResult> HomeSettingsVideoUpload(IFormFileCollection files)
		{
			string homeVideoPath = Path.Combine(_webHostEnvironment.WebRootPath, "home-video");

			string filesPath = Path.Combine(homeVideoPath);

			if (Directory.Exists(filesPath))
			{
				Directory.Delete(filesPath, true);

				string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "home-video");

				if (!Directory.Exists(uploadPath))
					Directory.CreateDirectory(uploadPath);

				async Task<bool> CopyFileAsync(string path, IFormFile file)
				{
					try
					{
						await using FileStream fileStream = new(
						 path,
						 FileMode.Create,
						 FileAccess.Write,
						 FileShare.None,
						 1024 * 1024,
						 useAsync: false);
						await file.CopyToAsync(fileStream);
						await fileStream.FlushAsync();
						return true;
					}
					catch (Exception e)
					{
						throw e;
					}
				}
				List<(string fileName, string path)> datas = new();
				foreach (IFormFile file in files)
				{
					string fileNewName = "homevideo.mp4";
					await CopyFileAsync($"{uploadPath}\\{fileNewName}", file);
					datas.Add((fileNewName, $"home-video\\{fileNewName}"));

				}

			}
			else
			{
				string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "home-video");

				if (!Directory.Exists(uploadPath))
					Directory.CreateDirectory(uploadPath);

				async Task<bool> CopyFileAsync(string path, IFormFile file)
				{
					try
					{
						await using FileStream fileStream = new(
						 path,
						 FileMode.Create,
						 FileAccess.Write,
						 FileShare.None,
						 1024 * 1024,
						 useAsync: false);
						await file.CopyToAsync(fileStream);
						await fileStream.FlushAsync();
						return true;
					}
					catch (Exception e)
					{
						throw e;
					}
				}
				List<(string fileName, string path)> datas = new();
				foreach (IFormFile file in files)
				{
					string fileNewName = "homevideo.mp4";
					await CopyFileAsync($"{uploadPath}\\{fileNewName}", file);
					datas.Add((fileNewName, $"home-video\\{fileNewName}"));

				}

			}

			return Ok();
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> GetSetting()
		{
			HomeSetting homeSetting = await _homeSettingReadRepositories.Table.FirstOrDefaultAsync();
			if (homeSetting == null)
			{
				HomeSetting homeSetting1 = new()
				{
					Id = Guid.NewGuid(),
					WelcomeText = "string",
					WelcomeTitle = "string",
					NumberOfFeaturedProducts = 6,
					ContactAddress= "1234 Bootstrap Caddesi Sıfır Blok, Bootstrap Mahallesi BootCity, BC 54321 Ülke: Bootland",
					ContactMail= "info@cartopia.com",
					ContactNumber= "+90555 555 5555"
				};
				await _homeSettingWriteRepositories.AddAsync(homeSetting1);
				await _homeSettingWriteRepositories.SaveAsync();
			}
			return Ok(homeSetting);
		}

		[HttpGet("[action]")]
		public async Task<IActionResult>UpdateHomeTitle([FromQuery]string title)
		{
			HomeSetting homeSetting = await _homeSettingReadRepositories.Table.FirstOrDefaultAsync();
			homeSetting.WelcomeTitle = title;
			await _homeSettingWriteRepositories.SaveAsync();
			return Ok();
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> UpdateHomeText([FromQuery] string text)
		{
			HomeSetting homeSetting = await _homeSettingReadRepositories.Table.FirstOrDefaultAsync();
			homeSetting.WelcomeText = text;
			await _homeSettingWriteRepositories.SaveAsync();
			return Ok();

		}

		[HttpGet("[action]")]
		public async Task<IActionResult> UpdateFeaturedProduct([FromQuery] int size)
		{
			HomeSetting homeSetting = await _homeSettingReadRepositories.Table.FirstOrDefaultAsync();
			homeSetting.NumberOfFeaturedProducts = size;
			await _homeSettingWriteRepositories.SaveAsync();
			return Ok();
		}


		[HttpGet("[action]")]
		public async Task<IActionResult> UpdateContactAddress([FromQuery] string address)
		{
			HomeSetting homeSetting = await _homeSettingReadRepositories.Table.FirstOrDefaultAsync();
			homeSetting.ContactAddress = address;
			await _homeSettingWriteRepositories.SaveAsync();
			return Ok();
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> UpdateContactMail([FromQuery] string mail)
		{
			HomeSetting homeSetting = await _homeSettingReadRepositories.Table.FirstOrDefaultAsync();
			homeSetting.ContactMail = mail;
			await _homeSettingWriteRepositories.SaveAsync();
			return Ok();
		}
		[HttpGet("[action]")]
		public async Task<IActionResult> UpdateContactNumber([FromQuery] string number)
		{
			HomeSetting homeSetting = await _homeSettingReadRepositories.Table.FirstOrDefaultAsync();
			homeSetting.ContactNumber = number;
			await _homeSettingWriteRepositories.SaveAsync();
			return Ok();
		}
	}
}
