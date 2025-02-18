using Microsoft.Azure.ServiceBus;

namespace OnlineMarketplace.Server.Interface
{
    public interface IOMQueueClient
    {
        Task SendAsync(Message message );
        Task CloseAsync();
    }

}