using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using OnlineMarketplace.Server.Models;

namespace OnlineMarketplace.Server.Data.Repositories
{
    public class ProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // Create and return an open connection
        private IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            using (var connection = CreateConnection())
            {
                var sql = "SELECT * FROM Products";
                return await connection.QueryAsync<Product>(sql);
            }
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            using (var connection = CreateConnection())
            {
                var sql = "SELECT * FROM Products WHERE Id = @Id";
                return await connection.QueryFirstOrDefaultAsync<Product>(sql, new { Id = id });
            }
        }

        public async Task<int> CreateProductAsync(Product product)
        {
            using (var connection = CreateConnection())
            {
                var sql = @"
                    INSERT INTO Products (Name, Description, Price, CreatedAt, ImageUrl)
                    VALUES (@Name, @Description, @Price, @CreatedAt, @ImageUrl);
                    SELECT CAST(SCOPE_IDENTITY() as int)";
                return await connection.QuerySingleAsync<int>(sql, product);
            }
        }

        public async Task<int> UpdateProductAsync(Product product)
        {
            using (var connection = CreateConnection())
            {
                var sql = @"
                    UPDATE Products
                    SET Name = @Name,
                        Description = @Description,
                        Price = @Price,
                        CreatedAt = @CreatedAt,
                        ImageUrl = @ImageUrl
                    WHERE Id = @Id";
                return await connection.ExecuteAsync(sql, product);
            }
        }

        public async Task<int> DeleteProductAsync(int id)
        {
            using (var connection = CreateConnection())
            {
                var sql = "DELETE FROM Products WHERE Id = @Id";
                return await connection.ExecuteAsync(sql, new { Id = id });
            }
        }
    }
}
