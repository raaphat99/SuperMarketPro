using Microsoft.EntityFrameworkCore;
using MVC.Models;
using MVC.ViewModels;

namespace MVC.Data
{
    public class ApplicationContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public ApplicationContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Category table with some initial data
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics", Description = "Devices and gadgets" },
                new Category { Id = 2, Name = "Clothing", Description = "Apparel and fashion" },
                new Category { Id = 3, Name = "Books", Description = "Books of all genres" },
                new Category { Id = 4, Name = "Home & Kitchen", Description = "Furniture and kitchenware" }
            );

            modelBuilder.Entity<Product>()
                .HasAnnotation("SqlServer:Identity", "100, 1");
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
