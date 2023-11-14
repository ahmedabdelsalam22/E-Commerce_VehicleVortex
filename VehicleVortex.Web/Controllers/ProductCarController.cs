using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VehicleVortex.Web.Models;
using VehicleVortex.Web.Models.Dto;
using VehicleVortex.Web.Service.IServices;

namespace VehicleVortex.Web.Controllers
{
    public class ProductCarController : Controller
    {
        private readonly IProductCarRestService _carRestService;
        private readonly IMapper _mapper;
        public ProductCarController(IProductCarRestService carRestService,IMapper mapper)
        {
            _carRestService = carRestService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<ProductCar> productCars = await _carRestService.GetAllAsync("api/allcars");

            var productCarDtos = _mapper.Map<List<ProductCarDto>>(productCars);

            return View(productCarDtos);
        }
    }
}
