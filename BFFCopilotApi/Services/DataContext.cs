using BFFCopilotApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BFFCopilotApi.Services
{
    public class DataContext : IdentityDbContext<ApplicationUser> // Use your ApplicationUser class
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        // Override OnModelCreating if you need to configure model properties or relationships further
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for ApplicationUser
            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = 1, // Ensure this ID is unique and not used by any existing records
                    Username = "admin",
                    Password = "admin" // This should be hashed and salted in a real application
                }
            );

            // Seed data for User
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    Name = "admin",
                    Mobile = "admin" 
                }
            );

            // Seed data for Product
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = 1,
                    Description = "Sample Product",
                    Price = 9.99M,
                    ProductType = "A",
                    Stock = 10
                }
            );

            // Seed data for CartItem
            modelBuilder.Entity<CartItem>().HasData(
                new CartItem
                {
                    CartItemId = 1,
                    CustomerId = 1,
                    ProductId = 1,
                    Quantity = 1
                }
            );
        }
    }
}
