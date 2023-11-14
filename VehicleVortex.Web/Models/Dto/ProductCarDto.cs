namespace VehicleVortex.Web.Models.Dto
{
    public class ProductCarDto
    {
        public int Id { get; set; } 
        public string Make { get; set; } 
        public string Model { get; set; } 
        public int Year { get; set; } 
        public string Color { get; set; } 
        public string Vin { get; set; } 
        public string Description { get; set; }
        public decimal Price { get; set; } 
        public int Mileage { get; set; } 
        public string TransmissionType { get; set; } 
        public string FuelType { get; set; } 
        public string ImageUrl { get; set; } 
        public DateTime ListingDate { get; set; }
    }
}
