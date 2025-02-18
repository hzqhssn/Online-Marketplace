using System.Text.Json;
using System.Text;
using OnlineMarketplace.Server.Interface;
using Microsoft.Azure.ServiceBus;
using OnlineMarketplace.Server.Models;

public class PaymentService : IPaymentService
{
    private readonly IOMQueueClient _queueClient;

    // Constructor injecting the IQueueClient dependency.
    public PaymentService(IOMQueueClient queueClient)
    {
        _queueClient = queueClient;
    }

    // Asynchronously processes a payment.
    public async Task ProcessPaymentAsync(PaymentDto paymentDto)
    {
        // Simulate payment processing logic here...
        // (In a real implementation, integrate with a payment gateway.)

        // After processing, create an event to update the order status.
        var orderUpdateEvent = new OrderUpdateEvent
        {
            OrderId = paymentDto.OrderId,
            PaymentStatus = "Paid"
        };

        // Serialize the event object to a JSON string.
        var messageBody = JsonSerializer.Serialize(orderUpdateEvent);
        // Create a new message with the serialized event.
        var message = new Message(Encoding.UTF8.GetBytes(messageBody));

        // Publish the message to the queue asynchronously.
        await _queueClient.SendAsync(message);
    }
}

