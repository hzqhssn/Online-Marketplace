using Microsoft.EntityFrameworkCore;
using OnlineMarketplace.Server.Models; 

namespace OnlineMarketplace.Server.Data
{
	public class UserDbContext : DbContext
	{
		public UserDbContext(DbContextOptions<UserDbContext> options)
			: base(options)
		{
		}

		public DbSet<User> Users { get; set; }
	}
}
