using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VehicleVortex.Data;
using VehicleVortex.Models.Dto;
using VehicleVortex.Models.Order;
using VehicleVortex.Models.ShoppingCart;
using VehicleVortex.Services.IGenericRepositories;
using VehicleVortex.Utilities;

namespace VehicleVortex.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private IMapper _mapper;
        private ResponseDto _responseDto;

        public OrderController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _responseDto = new ResponseDto();
        }

        [Authorize]
        [HttpPost("CreateOrder")]
        public async Task<ResponseDto> CreateOrder([FromBody] CartDto cartDto) 
        {
            try 
            {
                OrderHeaderDto orderHeaderDto = _mapper.Map<OrderHeaderDto>(cartDto.CartHeaderDto);
                orderHeaderDto.OrderTime = DateTime.Now;
                orderHeaderDto.Status = SD.Status_Pending;

                orderHeaderDto.OrderDetailsDtos = _mapper.Map<IEnumerable<OrderDetailsDto>>(cartDto.CartDetailsDtos);

                OrderHeader orderHeader = _mapper.Map<OrderHeader>(orderHeaderDto);

                OrderHeader orderCreated = _context.OrderHeaders.Add(orderHeader).Entity;
                await _context.SaveChangesAsync();

                orderHeaderDto.OrderHeaderId = orderCreated.OrderHeaderId;
                _responseDto.Result = orderHeaderDto;
            }
            catch(Exception ex) 
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }


    }
}
