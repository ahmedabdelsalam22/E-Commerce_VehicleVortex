using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VehicleVortex.Models.Dto;

namespace VehicleVortex.Models.ShoppingCart
{
    public class CartDetails
    {
        [Key]
        public int CartDetailsId { get; set; }
        public int CartHeaderId { get; set; }
        [ForeignKey("CartHeaderId")]
        public CartHeader CartHeader { get; set; }
        public int ProductId { get; set; }
        [NotMapped]
        public ProductCarDto Product { get; set; }
        public int Count { get; set; }
    }
}
 