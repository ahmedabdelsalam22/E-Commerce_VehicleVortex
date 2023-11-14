using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VehicleVortex.Models;
using VehicleVortex.Models.Dto;
using VehicleVortex.Services.IGenericRepositories;

namespace VehicleVortex.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ProductCarController : ControllerBase
    {
        private readonly IProductCarRepository _carRepository;
        private readonly IMapper _mapper;

        public ProductCarController(IProductCarRepository carRepository,IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }

        [HttpGet("cars")]
        public async Task<IActionResult> GetAllCars()
        {
            IEnumerable<ProductCar> productCars = await _carRepository.GetAll(tracked: false);

            if (productCars == null) 
            {
                return NotFound("no cars founds");
            }

            var productCarDtos = _mapper.Map<List<ProductCarDto>>(productCars);

            return Ok(productCarDtos);
        }
    }
}
