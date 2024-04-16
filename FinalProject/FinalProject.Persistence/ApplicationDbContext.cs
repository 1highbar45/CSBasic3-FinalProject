using FinalProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace FinalProject.Persistence
{
    public class ApplicationDbContext : DbContext
	{

		public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Review> Reviews { get; set; }
		public DbSet<ProductImage> ProductImages { get; set; }
		public DbSet<Bill> Bills { get; set; }
		public DbSet<BillDetail> BillDetails { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{

		}

	}
}
