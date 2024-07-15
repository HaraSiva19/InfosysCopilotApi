using BFFCopilotApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BFFCopilotApi.Services
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        // Override OnModelCreating if you need to configure model properties or relationships further
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Model configuration examples
            // modelBuilder.Entity<User>().ToTable("Users");
        }
    }
}
