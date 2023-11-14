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
        public ProductCarController(IProductCarRestService carRestService, IMapper mapper)
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
        [HttpGet]
        public IActionResult CreateProductCar()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateProductCar(ProductCarDto carDto)
        {
            if (ModelState.IsValid)
            {
                ProductCar productCarToCreate = _mapper.Map<ProductCar>(carDto);

                var response = await _carRestService.PostAsync(url: "api/car/create", data: productCarToCreate);

                if (response.IsSuccessful)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(carDto);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProductCar(int? id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }

            ProductCar productCar = await _carRestService.GetByIdAsync($"api/car/{id}");
            if (productCar == null)
            {
                return BadRequest();
            }

            ProductCarDto productCarDto = _mapper.Map<ProductCarDto>(productCar);
            return View(productCarDto);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProductCar(ProductCarDto carDto)
        {
            if (ModelState.IsValid)
            {
                ProductCar productCarToUpdate = _mapper.Map<ProductCar>(carDto);

                var response = await _carRestService.UpdateAsync(url: $"api/car/update/{carDto.Id}", data: productCarToUpdate);

                if (response != null)
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            return View(carDto);
        }

        public async Task<IActionResult> DeleteProductCar(int? id)
        {
            if (id == null || id == 0) 
            {
                return BadRequest();
            }
            ProductCar productCar = await _carRestService.GetByIdAsync($"api/car/{id}");
            if (productCar == null)
            {
                return BadRequest();
            }
            await _carRestService.Delete(url:$"api/car/delete/{id}");

            return RedirectToAction("Index");
        }
    }
}
