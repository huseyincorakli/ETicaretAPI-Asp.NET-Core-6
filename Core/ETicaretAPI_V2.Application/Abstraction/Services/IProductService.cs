namespace ETicaretAPI_V2.Application.Abstraction.Services
{
    public interface IProductService
    {
        Task<byte[]> QRCodeToProductAsync(string productId);
    }
}
