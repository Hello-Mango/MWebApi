namespace QuickFireApi.SignalR
{
    public interface IChatClient
    {
        Task ReceiveMessage<T>(string MessageType, T message);
    }
}
