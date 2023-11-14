using VehicleVortex.Web.Models;
using VehicleVortex.Web.Service.IServices;

namespace VehicleVortex.Web.Service.ServicesImpl
{
    public class ProductCarRestService : RestService<ProductCar>, IProductCarRestService
    {
        public ProductCarRestService(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
