using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleVortex.Models.ShoppingCart
{
    public class CartHeaderDto
    {
        public int CartHeaderId { get; set; }
        public double CartTotal { get; set; }
        public string? UserId { get; set; }
    }
}
