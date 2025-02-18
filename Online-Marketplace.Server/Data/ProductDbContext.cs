using Microsoft.EntityFrameworkCore;
using OnlineMarketplace.Server.Models;

namespace OnlineMarketplace.Server.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed dummy product data
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Wireless Headphones",
                    Description = "High quality wireless headphones with noise cancellation.",
                    Price = 99.99m,
                    ImageUrl = "https://via.placeholder.com/150"
                },
                new Product
                {
                    Id = 2,
                    Name = "Smartwatch",
                    Description = "Water-resistant smartwatch with fitness tracking features.",
                    Price = 199.99m,
                    ImageUrl = "https://via.placeholder.com/150"
                },
                new Product
                {
                    Id = 3,
                    Name = "Portable Speaker",
                    Description = "Compact and powerful Bluetooth speaker for on-the-go music.",
                    Price = 49.99m,
                    ImageUrl = "https://via.placeholder.com/150"
                }
            );
        }
    }
}
