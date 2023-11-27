using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VehicleVortex.Models;
using VehicleVortex.Models.Order;
using VehicleVortex.Models.ShoppingCart;

namespace VehicleVortex.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<ProductCar> ProductCars { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<CartHeader> CartHeaders { get; set; }
        public DbSet<CartDetails> CartDetails { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
