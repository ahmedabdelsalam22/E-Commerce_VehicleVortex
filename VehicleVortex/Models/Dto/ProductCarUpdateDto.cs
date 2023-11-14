using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VehicleVortex.Models.Dto
{
    public class ProductCarUpdateDto
    {
        [JsonIgnore]
        public int? Id { get; set; }
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
