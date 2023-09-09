namespace ETicaretAPI_V2.Application.Abstraction.Services
{
    public interface IMailService
    {
        //Task SendMessageAsync(string[] tos, string subject, string body, bool isBodyHtml = true);
        Task SendMessageAsync(string to, string subject, string body, bool isBodyHtml = true);
    }
}
