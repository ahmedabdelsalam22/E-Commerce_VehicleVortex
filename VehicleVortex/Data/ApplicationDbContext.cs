using Microsoft.EntityFrameworkCore;
using VehicleVortex.Models;

namespace VehicleVortex.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<ProductCar> ProductCars { get; set; }
    }
}
