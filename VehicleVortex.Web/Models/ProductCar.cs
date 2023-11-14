using System.ComponentModel.DataAnnotations;

namespace VehicleVortex.Web.Models  
{
    public class ProductCar
    {
        public int Id { get; set; } // Unique identifier for the car listing
        public string Make { get; set; } // Make of the car (e.g., Ford, Toyota, BMW)
        public string Model { get; set; } // Model of the car (e.g., Mustang, Camry, X5)
        public int Year { get; set; } // Manufacturing year of the car
        public string Color { get; set; } // Color of the car
        public string Vin { get; set; } // Vehicle Identification Number (VIN)
        public string Description { get; set; } // Description or additional details about the car
        public decimal Price { get; set; } // Price of the car
        public int Mileage { get; set; } // Mileage of the car
        public string TransmissionType { get; set; } // Transmission type (e.g., Automatic, Manual)
        public string FuelType { get; set; } // Fuel type (e.g., Gasoline, Diesel)
        public string ImageUrl { get; set; } // URL to an image of the car
        public DateTime ListingDate { get; set; } // Date when the car listing was created
    }
}
