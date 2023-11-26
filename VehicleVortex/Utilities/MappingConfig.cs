using AutoMapper;
using VehicleVortex.Models;
using VehicleVortex.Models.Dto;
using VehicleVortex.Models.ShoppingCart;

namespace VehicleVortex.Utilities
{
    public class MappingConfig : Profile
    {
        public MappingConfig() 
        {
            CreateMap<ProductCar, ProductCarDto>();
            CreateMap<ProductCarCreateDto, ProductCar>();
            CreateMap<ProductCarUpdateDto, ProductCar>();

            CreateMap<AppUser, AppUserDto>();

            CreateMap<CartHeaderDto, CartHeader>();
            CreateMap<ProductCar, ProductCarDto>();

        }
    }
}
