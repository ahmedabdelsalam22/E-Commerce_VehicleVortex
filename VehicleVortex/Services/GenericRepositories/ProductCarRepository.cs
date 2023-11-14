using VehicleVortex.Data;
using VehicleVortex.Models;
using VehicleVortex.Services.IGenericRepositories;

namespace VehicleVortex.Services.GenericRepositories
{
    public class ProductCarRepository : GenericRepository<ProductCar>, IProductCarRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductCarRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task Update(ProductCar productCar)
        {
            _context.Update(productCar);
            await _context.SaveChangesAsync();
        }
    }
}
