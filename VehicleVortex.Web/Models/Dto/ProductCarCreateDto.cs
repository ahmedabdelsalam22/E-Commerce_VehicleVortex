using System.ComponentModel.DataAnnotations;

namespace VehicleVortex.Web.Models.Dto
{
    public class ProductCarCreateDto
    {
        [Required]
        public string Make { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        public string Vin { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Mileage { get; set; }
        [Required]
        public string TransmissionType { get; set; }
        [Required]
        public string FuelType { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public DateTime ListingDate { get; set; }
    }
}
