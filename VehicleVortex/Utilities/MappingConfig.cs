using AutoMapper;
using VehicleVortex.Models;
using VehicleVortex.Models.Dto;

namespace VehicleVortex.Utilities
{
    public class MappingConfig : Profile
    {
        public MappingConfig() 
        {
            CreateMap<ProductCar, ProductCarDto>();
            CreateMap<ProductCarCreateDto, ProductCar>();
            CreateMap<ProductCarUpdateDto, ProductCar>();
        }
    }
}
