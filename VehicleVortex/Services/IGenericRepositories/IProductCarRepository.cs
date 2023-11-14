using VehicleVortex.Models;

namespace VehicleVortex.Services.IGenericRepositories
{
    public interface IProductCarRepository : IGenericRepository<ProductCar>
    {
        Task Update(ProductCar productCar);
    }
}
