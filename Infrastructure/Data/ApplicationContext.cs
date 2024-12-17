using Domain.Models;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IConfiguration _configuration;
        public ApplicationContext(DbContextOptions<ApplicationContext> options, IConfiguration configuration)
            : base(options)
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

            // Seed Transaction table with some initial data
            modelBuilder.Entity<Transaction>().HasData(
                new Transaction
                {
                    TransactionId = 1,
                    TimeStamp = DateTime.Now,
                    ProductId = 1001,
                    ProductName = "Laptop",
                    Price = 1200.00,
                    AvailableQuantityBeforeTransaction = 50,
                    SoldQuantity = 1,
                    CashierName = "Alice"
                },
                new Transaction
                {
                    TransactionId = 2,
                    TimeStamp = DateTime.Now,
                    ProductId = 1002,
                    ProductName = "Smartphone",
                    Price = 800.00,
                    AvailableQuantityBeforeTransaction = 100,
                    SoldQuantity = 2,
                    CashierName = "Bob"
                },
                new Transaction
                {
                    TransactionId = 3,
                    TimeStamp = DateTime.Now,
                    ProductId = 1003,
                    ProductName = "Headphones",
                    Price = 200.00,
                    AvailableQuantityBeforeTransaction = 200,
                    SoldQuantity = 5,
                    CashierName = "Charlie"
                }
            );

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
