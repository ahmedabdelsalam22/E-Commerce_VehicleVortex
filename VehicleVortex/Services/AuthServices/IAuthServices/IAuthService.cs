using VehicleVortex.Models;
using VehicleVortex.Models.Dto;

namespace VehicleVortex.Services.AuthServices.IAuthServices
{
    public interface IAuthService
    {
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        string GenerateToken(AppUser user, IEnumerable<string> roles);
        bool IsUniqueUser(string username);
        Task<AppUserDto> Register(RegisterRequestDTO registerRequestDTO);

        Task<bool> AssignRole(string email, string roleName);
    }
}
