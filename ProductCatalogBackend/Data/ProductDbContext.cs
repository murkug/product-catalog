using Microsoft.EntityFrameworkCore;
using ProductCatalogBackend.Models;

namespace ProductCatalogBackend.Data
{
	public class ProductDbContext : DbContext
	{
		public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }

		public DbSet<Product> Products { get; set; }
		public DbSet<ProductType> ProductTypes { get; set; }
		public DbSet<Color> Colors { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Seed ProductType Data
			modelBuilder.Entity<ProductType>().HasData(
				new ProductType { Id = 1, Name = "Sofa" },
				new ProductType { Id = 2, Name = "Chair" },
				new ProductType { Id = 3, Name = "Table" },
				new ProductType { Id = 4, Name = "Bed" }
			);

			// Seed Color Data
			modelBuilder.Entity<Color>().HasData(
				new Color { Id = 1, Name = "Blue" },
				new Color { Id = 2, Name = "Ruby" },
				new Color { Id = 3, Name = "Gold" },
				new Color { Id = 4, Name = "White" },
				new Color { Id = 5, Name = "Black" },
				new Color { Id = 6, Name = "Green" }
			);
		}
	}
}
