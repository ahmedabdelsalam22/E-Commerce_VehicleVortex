using System.ComponentModel.DataAnnotations;

namespace VehicleVortex.Models
{
    public class ProductCar
    {
        [Key]
        public int Id { get; set; } // Unique identifier for the car listing
        [Required]
        public string Make { get; set; } // Make of the car (e.g., Ford, Toyota, BMW)
        [Required]
        public string Model { get; set; } // Model of the car (e.g., Mustang, Camry, X5)
        [Required]
        public int Year { get; set; } // Manufacturing year of the car
        [Required]
        public string Color { get; set; } // Color of the car
        [Required]
        public string Vin { get; set; } // Vehicle Identification Number (VIN)
        [Required]
        public string Description { get; set; } // Description or additional details about the car
        [Required]
        public decimal Price { get; set; } // Price of the car
        [Required]
        public int Mileage { get; set; } // Mileage of the car
        [Required]
        public string TransmissionType { get; set; } // Transmission type (e.g., Automatic, Manual)
        [Required]
        public string FuelType { get; set; } // Fuel type (e.g., Gasoline, Diesel)
        [Required]
        public string ImageUrl { get; set; } // URL to an image of the car
        [Required]
        public DateTime ListingDate { get; set; } // Date when the car listing was created
    }
}
