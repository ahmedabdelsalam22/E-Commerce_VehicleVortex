using AutoMapper;
using VehicleVortex.Web.Models;
using VehicleVortex.Web.Models.Dto;

namespace VehicleVortex.Web.Utilities
{
    public class MappingConfig : Profile
    {
        public MappingConfig() 
        {
            CreateMap<ProductCar,ProductCarDto>();
        }
    }
}
