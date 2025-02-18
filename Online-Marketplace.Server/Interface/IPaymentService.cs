using OnlineMarketplace.Server.Models;

namespace OnlineMarketplace.Server.Interface
{
    public interface IPaymentService
    {
        Task ProcessPaymentAsync(PaymentDto paymentDto);

    }
}
