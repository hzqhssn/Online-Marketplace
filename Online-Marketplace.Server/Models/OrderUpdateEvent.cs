namespace OnlineMarketplace.Server.Models
{
    internal class OrderUpdateEvent
    {
        public object OrderId { get; set; }
        public string PaymentStatus { get; set; }
    }
}