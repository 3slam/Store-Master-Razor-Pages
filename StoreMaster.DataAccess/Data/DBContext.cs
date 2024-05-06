using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using StoreMaster.Models;

namespace StoreMaster.DataAccess.Data
{
    public class DBContext : IdentityDbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Cart> ShoppingCart { get; set; }

		public virtual DbSet<OrderDetail> OrderDetails { get; set; }
		public virtual DbSet<OrderHeader> OrderHeaders { get; set; }

	}
}
