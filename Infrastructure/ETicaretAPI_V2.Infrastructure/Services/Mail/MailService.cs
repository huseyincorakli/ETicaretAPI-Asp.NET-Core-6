using ETicaretAPI_V2.Application.Abstraction.Services;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace ETicaretAPI_V2.Infrastructure.Services.Mail
{
    public class MailService : IMailService
    {
        readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        

        public async Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
        {
            using MailMessage mail = new();
            mail.IsBodyHtml= isBodyHtml;
            foreach (string to in tos)
                mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.From = new("hc.pc29@outlook.com","Cartopia",System.Text.Encoding.UTF8);

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
         public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendMailAsync(new[] {to},subject, body, isBodyHtml);
        }

        public async Task SendPasswordResetMailAsync(string to, string userId, string resetToken)
        {
            StringBuilder mail = new();
            mail.AppendLine("Merhaba,<br>");
            mail.AppendLine("Eğer yeni şifre talebinde bulunduysanız aşağıdaki linkten şifrenizi yenileyebilirsiniz.<br><br>Şifrenizi sıfırlamak için tıklayınız:");
            mail.AppendLine();
            mail.Append(_configuration["AngularClientUrl"]);
            mail.Append("/update-password/");
            mail.Append(userId);
            mail.Append("/");
            mail.Append(resetToken);
            mail.AppendLine();
            mail.AppendLine();
            mail.AppendLine("<br><br>Cartopia");

            string mailBody = mail.ToString();


            await SendMailAsync(to, "Şifre Yenileme Talebi", mailBody);
        }

        public async Task SendCompletedOrderMailAsync(string to, string orderCode, DateTime orderDate, string userName, string userSurname, string trackCode, string companyName,string companyUrl)
        {
            string mail = $"Sayın {userSurname.ToUpper()}-[{userName}],<br>" +
                $"{orderDate} tarihli , {orderCode} sipariş koduna sahip siparişiniz {companyName} adlı kargo firmasına teslim edilmiştir. Kargo Takip Numaranız : {trackCode}.<br>" +
                $"{companyUrl} adresinden siparişinizi takip edebilirsiniz <br>"+
                $"Bizi tercih ettiğiniz için teşekkür ederiz. <br>";
            await SendMailAsync(to, $"{orderCode} Sipariş Numaralı Siparişiniz Tamamlandı", mail);
        }

		public async Task ReplyToUserMailAsync(string to,string title,string messageContent)
		{
			string mail = $"Sayın {to},<br>" +
				$"{messageContent} <br>" +
				$"Bizi tercih ettiğiniz için teşekkür ederiz. <br>";
			await SendMailAsync(to, $"{title}", mail);
		}


	}
}



