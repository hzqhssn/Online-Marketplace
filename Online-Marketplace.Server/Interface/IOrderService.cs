using System.Threading.Tasks;
using OnlineMarketplace.Server.Models;

namespace OnlineMarketplace.Server.Interface
{
    public interface IOrderService
    {
        /// <summary>
        /// Creates a new order.
        /// </summary>
        Task<Order> CreateOrderAsync(OrderCreateDto orderDto);

        /// <summary>
        /// Retrieves an order by its ID.
        /// </summary>
        Task<Order> GetOrderAsync(int orderId);
    }
}
