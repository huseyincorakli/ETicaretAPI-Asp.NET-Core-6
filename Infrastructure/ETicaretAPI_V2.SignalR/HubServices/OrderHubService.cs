using ETicaretAPI.SignalR;
using ETicaretAPI_V2.Application.Abstraction.Hubs;
using ETicaretAPI_V2.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace ETicaretAPI_V2.SignalR.HubServices
{
    public class OrderHubService : IOrderHubService
    {
        readonly IHubContext<OrderHub> _hubContext;

        public OrderHubService(IHubContext<OrderHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task OrderAddedMessageAsync(string message)
        {
           await _hubContext.Clients.All.SendAsync(ReceiveFunctionNames.OrderAddedMessage,message);
        }
    }
}
