using AutoMapper;
using VehicleVortex.Models;
using VehicleVortex.Models.Dto;
using VehicleVortex.Models.Order;
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

            CreateMap<CartHeaderDto, CartHeader>().ReverseMap();
            CreateMap<CartDetailsDto, CartDetails>().ReverseMap();
            CreateMap<ProductCar, ProductCarDto>();


            CreateMap<OrderHeader, OrderHeaderDto>().ReverseMap();
            CreateMap<OrderDetailsDto, OrderDetails>().ReverseMap();

            CreateMap<OrderHeaderDto, CartHeaderDto>()
                .ForMember(dest => dest.CartTotal, x => x.MapFrom(src => src.OrderTotal)).ReverseMap();

            CreateMap<CartDetailsDto, OrderDetailsDto>()
               .ForMember(dest => dest.ProductName, x => x.MapFrom(src => src.Product.Model))
               .ForMember(dest => dest.Price, x => x.MapFrom(src => src.Product.Price));

            CreateMap<OrderDetailsDto, CartDetailsDto>();
        }
    }
}
