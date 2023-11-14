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

        [HttpGet("car/{id}")]
        public async Task<IActionResult> GetCarById(int? id)
        {
            if (id == null || id == 0) 
            {
                return BadRequest();
            }
            ProductCar productCar = await _carRepository.Get(tracked: false , filter:x=>x.Id == id);

            if (productCar == null)
            {
                return NotFound("no cars founds with this id");
            }

            var productCarDto = _mapper.Map<ProductCarDto>(productCar);

            return Ok(productCarDto);
        }

        [HttpGet("cars/{make}")]
        public async Task<IActionResult> GetCarsByMake(string? make)
        {
            if (make == null)
            {
                return BadRequest();
            }
            IEnumerable<ProductCar> productCars = await _carRepository.GetAll(tracked: false, filter:x=>x.Make.ToLower() == make.ToLower());

            if (productCars == null)
            {
                return NotFound($"no cars founds maken of {make}");
            }

            var productCarDtos = _mapper.Map<List<ProductCarDto>>(productCars);

            return Ok(productCarDtos);
        }
    }
}
