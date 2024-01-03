using ETicaretAPI_V2.Application.Abstraction.Services;
using ETicaretAPI_V2.Application.DTOs.Message;
using ETicaretAPI_V2.Application.Repositories.MessageRepositories;
using ETicaretAPI_V2.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ETicaretAPI_V2.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ContactController : ControllerBase
	{
		readonly IMessageReadRepository _messageReadRepository;
		readonly IMessageWriteRepository _messageWriteRepository;
		readonly IMailService _mailService;
		public ContactController(IMessageReadRepository messageReadRepository, IMessageWriteRepository messageWriteRepository, IMailService mailService)
		{
			_messageReadRepository = messageReadRepository;
			_messageWriteRepository = messageWriteRepository;
			_mailService = mailService;
		}


		[HttpPost("[action]")]
		public async Task<IActionResult> CreateMessage([FromBody] Create_Message message)
		{
			if (message != null)
			{
				Message message1 = new()
				{
					Id = Guid.NewGuid(),
					Email = message.Email,
					MessageContent = message.MessageContent,
					MessageTitle = message.MessageTitle
				};
				await _messageWriteRepository.AddAsync(message1);
				await _messageWriteRepository.SaveAsync();
			}
			else
			{
				return BadRequest("Mesaj Boş Olamaz");
			}
			return Ok();
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> GetAllMessage(int size)
		{
			if (size==null)
			{
				size = 6;
			}
			var data = await _messageReadRepository.GetAll().Take(size).ToListAsync();
			return Ok(data);
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> DeleteMessage([FromQuery]string id)
		{
			Message message = await _messageReadRepository.GetByIdAsync(id);
			if (message!=null)
			{
				_messageWriteRepository.Remove(message);
				await _messageWriteRepository.SaveAsync();
				return Ok();
			}
			else
			{
				return BadRequest();
			}
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> ReplyToMessage(string userMail,string title,string message)
		{
			await _mailService.ReplyToUserMailAsync(userMail,title, message);
			return Ok();
		}

	}
}
