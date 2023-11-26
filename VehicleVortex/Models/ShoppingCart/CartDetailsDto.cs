using System.ComponentModel.DataAnnotations.Schema;
using VehicleVortex.Models.Dto;

namespace VehicleVortex.Models.ShoppingCart
{
    public class CartDetailsDto
    {
        public int CartDetailsId { get; set; }
        public int CartHeaderId { get; set; }
        public CartHeaderDto? CartHeaderDto { get; set; }
        public int ProductId { get; set; }
        public ProductCarDto? Product { get; set; }
        public int Count { get; set; }
    }
}
