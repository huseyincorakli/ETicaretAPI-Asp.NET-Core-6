namespace ETicaretAPI_V2.Application.Exceptions
{
	public class NoStockException: Exception
	{
		public NoStockException() : base("Ürün stokta bulunmamaktadır.")
		{
		}

		public NoStockException(string? message) : base(message)
		{

		}

		public NoStockException(string? message, Exception? innerException) : base(message, innerException)
		{
		}
	}
}
