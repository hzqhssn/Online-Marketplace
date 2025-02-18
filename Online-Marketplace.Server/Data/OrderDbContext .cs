using Microsoft.EntityFrameworkCore;
using OnlineMarketplace.Server.Models;

namespace OnlineMarketplace.Server.Data
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options)
            : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
    }
}
