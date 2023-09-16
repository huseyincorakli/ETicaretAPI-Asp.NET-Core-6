namespace ETicaretAPI_V2.Application.Abstraction.Services
{
    public interface IProductService
    {
        Task<byte[]> QRCodeToProductAsync(string productId);
        Task UpdateProductStockAsync(string productId,int stock);
    }
}
