namespace ETicaretAPI_V2.Application.Abstraction.Services
{
    public interface IQRCodeService
    {
        byte[] GenerateQRCode(string text);
    }
}
