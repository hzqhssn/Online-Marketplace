namespace OnlineMarketplace.Server.Models
{
    public class PaymentDto
    {
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }  // e.g., "CreditCard", "PayPal"
    }
}
