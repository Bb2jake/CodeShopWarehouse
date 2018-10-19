using CodeShopWarehouse.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodeShopWarehouse.Data
{
	public class CodeShopWarehouseContext : DbContext
	{
		public CodeShopWarehouseContext(DbContextOptions options) : base(options) { }

		public DbSet<Order> Orders { get; set; }
	}
}
