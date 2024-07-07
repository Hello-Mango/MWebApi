using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace QuickFire.Infrastructure.SignalR
{
    public class ChatHub : Hub<IChatClient>
    {
        public async Task SendMessageToAll(string MessageType, object message)
            => await Clients.All.ReceiveMessage(MessageType, message);
    }

}
