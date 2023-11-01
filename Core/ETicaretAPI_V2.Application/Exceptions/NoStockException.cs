namespace ETicaretAPI_V2.Application.Exceptions
{
	public class NoStockException: Exception
	{
		public NoStockException() : base("Not enough stock available")
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
