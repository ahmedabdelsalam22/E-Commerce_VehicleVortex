using VehicleVortex.Models.Dto;

namespace VehicleVortex.Models
{
    public class LoginResponseDTO
    {
        public AppUserDto? appUserDto { get; set; }
        public string? Token { get; set; }
    }
}
