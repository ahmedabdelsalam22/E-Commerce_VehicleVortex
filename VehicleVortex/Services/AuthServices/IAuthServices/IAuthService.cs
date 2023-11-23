using VehicleVortex.Models;

namespace VehicleVortex.Services.AuthServices.IAuthServices
{
    public interface IAuthService
    {
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        string GenerateToken(AppUser user, IEnumerable<string> roles);
    }
}
