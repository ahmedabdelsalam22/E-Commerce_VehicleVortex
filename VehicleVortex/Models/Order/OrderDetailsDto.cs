using System.ComponentModel.DataAnnotations.Schema;
using VehicleVortex.Models.Dto;

namespace VehicleVortex.Models.Order
{
    public class OrderDetailsDto
    {
        public int OrderDetailsId { get; set; }
        public int OrderHeaderId { get; set; }
        public int ProductId { get; set; }
        public ProductCarDto? Product { get; set; }
        public int Count { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
    }
}
