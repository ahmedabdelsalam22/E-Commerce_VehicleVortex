using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;
using VehicleVortex.Data;
using VehicleVortex.Models;
using VehicleVortex.Models.Dto;
using VehicleVortex.Models.ShoppingCart;
using VehicleVortex.Services.IGenericRepositories;

namespace VehicleVortex.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private IMapper _mapper;
        private ResponseDto _responseDto;
        private IProductCarRepository _carRepository;

        public ShoppingCartController(ApplicationDbContext context, IMapper mapper, IProductCarRepository productCarRepository)
        {
            _context = context;
            _mapper = mapper;
            _responseDto = new ResponseDto();
            _carRepository = productCarRepository;
        }
        [Authorize]
        [HttpPost("cartUpsert")]
        public async Task<ResponseDto> CartUpsert(CartDto cartDto) 
        {
            try
            {
                var cartHeaderFromDb = await _context.CartHeaders.AsNoTracking().FirstOrDefaultAsync(x=>x.UserId == cartDto.CartHeaderDto!.UserId);

                if (cartHeaderFromDb == null)
                {
                    // create cartHeader and CartDetails

                    CartHeader cartHeader = _mapper.Map<CartHeader>(cartDto.CartHeaderDto);

                    await _context.CartHeaders.AddAsync(cartHeader);
                    await _context.SaveChangesAsync();
                    
                    cartDto.CartDetailsDtos!.First().CartHeaderId = cartHeader.CartHeaderId;

                    CartDetails cartDetails = _mapper.Map<CartDetails>(cartDto.CartDetailsDtos!.First());
                    await _context.CartDetails.AddAsync(cartDetails);
                    await _context.SaveChangesAsync();
                }
                else 
                {
                    // check if this product is exists or not

                    var cartDetailsFromDb = await _context.CartDetails.AsNoTracking().FirstOrDefaultAsync(
                        x=>x.ProductId == cartDto.CartDetailsDtos!.First().ProductId &&
                    x.CartHeaderId == cartDto.CartHeaderDto!.CartHeaderId);

                    if (cartDetailsFromDb == null)
                    {
                        // create CartDetails
                        cartDto.CartDetailsDtos!.First().CartHeaderId = cartHeaderFromDb.CartHeaderId;
                        CartDetails cartDetails = _mapper.Map<CartDetails>(cartDto.CartDetailsDtos!.First());
                        await _context.CartDetails.AddAsync(cartDetails);
                        await _context.SaveChangesAsync();
                    }
                    else 
                    {
                        // increment Count prop in cartDetails with "one"

                        cartDto.CartDetailsDtos!.First().Count += cartDetailsFromDb.Count;
                        cartDto.CartDetailsDtos!.First().CartDetailsId = cartDetailsFromDb.CartDetailsId;
                        cartDto.CartDetailsDtos!.First().CartHeaderId = cartDetailsFromDb.CartHeaderId;

                        CartDetails cartDetails = _mapper.Map<CartDetails>(cartDto.CartDetailsDtos!.First());
                        _context.CartDetails.Update(cartDetails);
                        await _context.SaveChangesAsync();
                    }
                }
                _responseDto.Result = cartDto;
            }
            catch (Exception ex) 
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        [Authorize]
        [HttpPost("RemoveCart")]
        public async Task<IActionResult> RemoveCart([FromBody] int cartDetailsId)
        {
            try
            {
                CartDetails cartDetails = await _context.CartDetails.FirstAsync(x => x.CartDetailsId == cartDetailsId);

                int totalCountofCartItem = _context.CartDetails.Where(u => u.CartHeaderId == cartDetails.CartHeaderId).Count();

                _context.CartDetails.Remove(cartDetails);

                if (totalCountofCartItem == 1)
                {
                    var cartHeaderToRemove = await _context.CartHeaders
                       .FirstOrDefaultAsync(u => u.CartHeaderId == cartDetails.CartHeaderId);

                    _context.CartHeaders.Remove(cartHeaderToRemove);
                }
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Authorize]
        [HttpGet("getCart/{userId}")]
        public async Task<ActionResult> GetCart(string userId)
        {
            try
            {
                CartDto cart = new()
                {
                    CartHeaderDto = _mapper.Map<CartHeaderDto>(_context.CartHeaders.First(u => u.UserId == userId))
                };
                cart.CartDetailsDtos = _mapper.Map<IEnumerable<CartDetailsDto>>(_context.CartDetails
                    .Where(u => u.CartHeaderId == cart.CartHeaderDto.CartHeaderId));

                IEnumerable<ProductCar> productCars = await _carRepository.GetAll(tracked: false);

                List<ProductCarDto> productDtos = _mapper.Map<List<ProductCarDto>>(productCars);

                foreach (var item in cart.CartDetailsDtos)
                {
                    item.Product = productDtos.FirstOrDefault(u => u.Id == item.ProductId);

                    cart.CartHeaderDto.CartTotal += (item.Count * item.Product.Price); // here we should get product from "ProductAPI" which means microservices. 
                }

                return Ok(cart);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

    }
}
