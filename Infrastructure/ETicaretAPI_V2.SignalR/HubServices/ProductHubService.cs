using ETicaretAPI_V2.Application.Abstractions.Hubs;
using ETicaretAPI_V2.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace ETicaretAPI.SignalR.HubServices
{
    public class ProductHubService : IProductHubService
    {
        readonly IHubContext<ProductHub> _hubContext;

        public ProductHubService(IHubContext<ProductHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task ProductAddedMessageAsync(string message)
        {
            await _hubContext.Clients.All.SendAsync(ReceiveFunctionNames.ProductAddedMessage, message);
        }
    }
}