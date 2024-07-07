using System.Threading.Tasks;

namespace QuickFire.Infrastructure.SignalR
{
    public interface IChatClient
    {
        Task ReceiveMessage<T>(string MessageType, T message);
    }
}
