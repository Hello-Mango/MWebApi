using Microsoft.AspNetCore.SignalR;

namespace MWebApi
{
    public class ChatHub : Hub<IChatClient>
    {
        public async Task SendMessageToAll<T>(string MessageType, T message)
            => await Clients.All.ReceiveMessage(MessageType, message);
    }
    public interface IChatClient
    {
        Task ReceiveMessage<T>(string MessageType, T message);
    }
}
