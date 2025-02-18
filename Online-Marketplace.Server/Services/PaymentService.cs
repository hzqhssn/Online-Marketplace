using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using OnlineMarketplace.Server.Interface;
using OnlineMarketplace.Server.Models;

namespace Online_Marketplace.Server.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IQueueClient _queueClient;

        public PaymentService(IQueueClient queueClient)
        {
            _queueClient = queueClient ?? throw new ArgumentNullException(nameof(queueClient));
        }

        public async Task ProcessPaymentAsync(PaymentDto paymentDto)
        {
            if (paymentDto == null)
            {
                throw new ArgumentNullException(nameof(paymentDto));
            }

            // Simulate processing the payment (this could include validation, communicating with a gateway, etc.)
            // For example, assume the payment is processed successfully

            // Create an event to update the order status
            var orderUpdateEvent = new OrderUpdateEvent
            {
                OrderId = paymentDto.OrderId,
                PaymentStatus = "Paid"
            };

            // Serialize the event to JSON
            var messageBody = JsonSerializer.Serialize(orderUpdateEvent);
            var message = new Message(Encoding.UTF8.GetBytes(messageBody));

            // Send the message to the queue
            await _queueClient.SendAsync(message);
        }
    }

    // This is a sample event class used to notify the order service of the payment status.
    public class OrderUpdateEvent
    {
        public int OrderId { get; set; }
        public string PaymentStatus { get; set; }
    }
}
