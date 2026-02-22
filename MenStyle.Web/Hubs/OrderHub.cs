using Microsoft.AspNetCore.SignalR;

namespace MenStyle.Web.Hubs
{
    public class OrderHub : Hub
    {
        public async Task SendOrderNotification(string message, int orderId)
        {
            await Clients.All.SendAsync("ReceiveOrderNotification", message, orderId);
        }
    }
}
