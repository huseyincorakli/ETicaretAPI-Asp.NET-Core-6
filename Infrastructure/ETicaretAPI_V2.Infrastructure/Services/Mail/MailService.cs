using ETicaretAPI_V2.Application.Abstraction.Services;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace ETicaretAPI_V2.Infrastructure.Services.Mail
{
    public class MailService : IMailService
    {
        readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendMessageAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
        {
            using MailMessage mail = new();
            mail.IsBodyHtml= isBodyHtml;
            foreach (string to in tos)
                mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.From = new("hc.pc29@outlook.com","CART CURT E-TİCARET",System.Text.Encoding.UTF8);

            using SmtpClient smtp = new();
            smtp.Credentials = new NetworkCredential(_configuration["Mail:Address"], _configuration["Mail:Password"]);
            smtp.Port = 587;
            smtp.EnableSsl=true;
            smtp.Host = "smtp-mail.outlook.com";

            try
            {
                await smtp.SendMailAsync(mail);

            }
            catch (Exception e)
            {
                throw  e;
            }
        }
         public async Task SendMessageAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendMessageAsync(new[] {to},subject, body, isBodyHtml);
        }
    }
}



