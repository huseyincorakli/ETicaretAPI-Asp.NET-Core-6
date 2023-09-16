using ETicaretAPI_V2.Application.Abstraction.Services;
using QRCoder;

namespace ETicaretAPI_V2.Infrastructure.Services
{
    public class QRCodeService : IQRCodeService
    {
        public QRCodeService()
        {


        }

        public byte[] GenerateQRCode(string text)
        {
            QRCodeGenerator generator = new QRCodeGenerator();
            QRCodeData data = generator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode qrCode = new(data);

            byte[] byteGraphic = qrCode.GetGraphic(10, new byte[] { 0, 0, 0 }, new byte[] { 255, 255, 255 });
            
            return byteGraphic;
        }
    }
}
