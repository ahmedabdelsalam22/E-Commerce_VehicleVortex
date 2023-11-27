using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using VehicleVortex.Data;
using VehicleVortex.Models.Dto;
using VehicleVortex.Models.Order;
using VehicleVortex.Models.Payment;
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

        [Authorize]
        [HttpPost("CreateStripeSession")]
        public async Task<ActionResult<StripeRequestDto>> CreateStripeSession([FromBody] StripeRequestDto stripeRequestDto)
        {

            var options = new SessionCreateOptions
            {
                SuccessUrl = stripeRequestDto.ApprovedUrl,
                CancelUrl = stripeRequestDto.CancelUrl,
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
            };


            foreach (var item in stripeRequestDto.OrderHeaderDto.OrderDetailsDtos)
            {
                var sessionLineItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.Price * 100), // if price 20$ , the unitAmout will be 20.00
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.ProductName
                        }
                    },
                    Quantity = item.Count
                };
                options.LineItems.Add(sessionLineItem);
            }

            var service = new SessionService();
            Session session = service.Create(options);

            stripeRequestDto.StripeSessionUrl = session.Url;


            OrderHeader orderHeader = _context.OrderHeaders.First(x => x.OrderHeaderId == stripeRequestDto.OrderHeaderDto.OrderHeaderId);

            orderHeader.StripeSessionId = session.Id;
            _context.SaveChanges();

            return Ok(stripeRequestDto);
        }

        [Authorize]
        [HttpPost("ValidateStripeSession/{orderHeaderId}")]
        public async Task<ActionResult<OrderHeaderDto>> ValidateStripeSession([FromBody] int orderHeaderId)
        {
            try
            {

                OrderHeader orderHeader = _context.OrderHeaders.First(u => u.OrderHeaderId == orderHeaderId);

                var service = new SessionService();
                Session session = service.Get(orderHeader.StripeSessionId);

                //PaymentIntent obj to can access payment status 
                var paymentIntentService = new PaymentIntentService();
                PaymentIntent paymentIntent = paymentIntentService.Get(session.PaymentIntentId);

                if (paymentIntent.Status == "succeeded")
                {
                    //then payment was successful
                    orderHeader.PaymentIntentId = paymentIntent.Id;
                    orderHeader.Status = SD.Status_Approved;
                    _context.SaveChanges();

                }
                return Ok(_mapper.Map<OrderHeaderDto>(orderHeader));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
