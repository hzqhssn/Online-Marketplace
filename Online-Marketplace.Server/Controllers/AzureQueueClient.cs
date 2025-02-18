using Microsoft.Azure.ServiceBus;
using OnlineMarketplace.Server.Interface; 

namespace OnlineMarketplace.Server.Controllers
{
    public class AzureQueueClient:IOMQueueClient
    {

        private readonly QueueClient _queueClient;

        // Constructor that initializes the QueueClient with connection string and queue name.
        public AzureQueueClient(string serviceBusConnectionString, string queueName)
        {
            if (string.IsNullOrEmpty(serviceBusConnectionString))
                throw new ArgumentNullException(nameof(serviceBusConnectionString));
            if (string.IsNullOrEmpty(queueName))
                throw new ArgumentNullException(nameof(queueName));

            _queueClient = new QueueClient(serviceBusConnectionString, queueName);
        }

        // Sends a message asynchronously using the underlying QueueClient.
        public async Task SendAsync(Message message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            await _queueClient.SendAsync(message);
        }

        // Closes the QueueClient connection asynchronously.
        public async Task CloseAsync()
        {
            await _queueClient.CloseAsync();
        }


    }
}
