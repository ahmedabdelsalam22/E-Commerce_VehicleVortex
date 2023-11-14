using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

        [HttpGet("allcars")]
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

        [HttpGet("allcars/{model}")]
        public async Task<IActionResult> GetCarsByModel(string? model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            IEnumerable<ProductCar> productCars = await _carRepository.GetAll(tracked: false, filter: x => x.Model.ToLower() == model.ToLower());

            if (productCars == null)
            {
                return NotFound($"no cars founds this {model} model");
            }

            var productCarDtos = _mapper.Map<List<ProductCarDto>>(productCars);

            return Ok(productCarDtos);
        }

        [HttpGet("getallcars/{fueltype}")]
        public async Task<IActionResult> GetCarsByFuelType(string? fueltype)
        {
            if (fueltype == null)
            {
                return BadRequest();
            }
            IEnumerable<ProductCar> productCars = await _carRepository.GetAll(tracked: false, filter: x => x.FuelType.ToLower() == fueltype.ToLower());

            if (productCars == null)
            {
                return NotFound($"no cars founds this {fueltype} fueltype");
            }

            var productCarDtos = _mapper.Map<List<ProductCarDto>>(productCars);

            return Ok(productCarDtos);
        }

        [HttpPost("car/create")]
        public async Task<IActionResult> CreateCar([FromBody] ProductCarCreateDto createDto)
        {
            if (ModelState.IsValid) 
            {
                ProductCar productCar = await _carRepository.Get(filter:x=>x.Vin.ToLower() == createDto.Vin.ToLower());

                if (productCar != null) 
                {
                    return BadRequest("car already exists");
                }

                ProductCar productCarToCreate = _mapper.Map<ProductCar>(createDto);

                await _carRepository.Create(productCarToCreate);

                return Ok();
            }
            return BadRequest("model is not valid");
        }

        [HttpPut("car/update/{id}")]
        public async Task<IActionResult> UpdateCar(int? id, [FromBody] ProductCarUpdateDto updateDto) 
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                ProductCar productCar = await _carRepository.Get(filter: x => x.Id == id , tracked:false);

                if(productCar == null)
                {
                    return BadRequest();
                }

                updateDto.Id = id;

                ProductCar productCarToUpdate = _mapper.Map<ProductCar>(updateDto);

                await _carRepository.Update(productCarToUpdate);

                return Ok();
            }
            return BadRequest("model is not valid");
        }

        [HttpDelete("car/delete/{id}")]
        public async Task<IActionResult> DeleteCar(int? id) 
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            ProductCar productCar = await _carRepository.Get(tracked: false, filter: x => x.Id == id);

            if (productCar == null)
            {
                return NotFound("no cars founds to delete");
            }

            await _carRepository.Delete(productCar);
            return Ok();
        }
    }
}
