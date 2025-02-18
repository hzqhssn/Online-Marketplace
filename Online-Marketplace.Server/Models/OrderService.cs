using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using OnlineMarketplace.Server.Data;
using OnlineMarketplace.Server.Interface;
using OnlineMarketplace.Server.Models;

namespace OnlineMarketplace.Server.Services
{
    public class OrderService : IOrderService
    {
        private readonly OrderDbContext _dbContext;
        private readonly IDistributedCache _cache;
        private const string CacheKeyPrefix = "order_";

        public OrderService(OrderDbContext dbContext, IDistributedCache cache)
        {
            _dbContext = dbContext;
            _cache = cache;
        }

        public async Task<Order> CreateOrderAsync(OrderCreateDto orderDto)
        {
            // Create a new order
            var order = new Order
            {
                UserId = orderDto.UserId,
                TotalAmount = orderDto.TotalAmount,
                OrderDate = DateTime.UtcNow,
                // Initialize additional properties as needed
            };

            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();

            // Optionally, cache the new order
            string cacheKey = $"{CacheKeyPrefix}{order.Id}";
            var serializedOrder = JsonSerializer.Serialize(order);
            await _cache.SetStringAsync(cacheKey, serializedOrder);

            return order;
        }

        public async Task<Order> GetOrderAsync(int orderId)
        {
            string cacheKey = $"{CacheKeyPrefix}{orderId}";

            // Try to retrieve the order from the cache first
            var cachedOrder = await _cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedOrder))
            {
                return JsonSerializer.Deserialize<Order>(cachedOrder);
            }

            // If not in cache, retrieve it from the Data
            var order = await _dbContext.Orders.FindAsync(orderId);
            if (order != null)
            {
                // Cache the order for subsequent requests
                var serializedOrder = JsonSerializer.Serialize(order);
                await _cache.SetStringAsync(cacheKey, serializedOrder);
            }

            return order;
        }
    }
}
