using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleVortex.Models.ShoppingCart
{
    public class CartHeader
    {
        [Key]
        public int CartHeaderId { get; set; }
        [NotMapped]
        public double CartTotal { get; set; }
        public int? UserId { get; set; }
    }
}
