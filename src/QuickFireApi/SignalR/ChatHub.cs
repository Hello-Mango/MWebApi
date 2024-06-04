using Microsoft.AspNetCore.SignalR;

namespace QuickFireApi.SignalR
{
    public class ChatHub : Hub<IChatClient>
    {
        public async Task SendMessageToAll(string MessageType, object message)
            => await Clients.All.ReceiveMessage(MessageType, message);
    }

}
