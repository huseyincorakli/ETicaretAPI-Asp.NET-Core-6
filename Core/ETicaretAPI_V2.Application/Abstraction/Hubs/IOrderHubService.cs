namespace ETicaretAPI_V2.Application.Abstraction.Hubs
{
    public interface IOrderHubService
    {
        Task OrderAddedMessageAsync(string message);
    }
}
