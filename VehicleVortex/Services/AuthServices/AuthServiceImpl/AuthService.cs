using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VehicleVortex.Data;
using VehicleVortex.Models;
using VehicleVortex.Models.Dto;
using VehicleVortex.Services.AuthServices.IAuthServices;
using VehicleVortex.Utilities;

namespace VehicleVortex.Services.AuthServices.AuthServiceImpl
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtOptions _jwtOptions;

        public AuthService(ApplicationDbContext dbContext, RoleManager<IdentityRole> roleManager,
            UserManager<AppUser> userManager, IMapper mapper, IOptions<JwtOptions> jwtOptions)
        {
            _dbContext = dbContext;
            this._roleManager = roleManager;
            this._userManager = userManager;
            _mapper = mapper;
            _jwtOptions = jwtOptions.Value;
        }

        public bool IsUniqueUser(string username)
        {
            var user = _dbContext.AppUsers.FirstOrDefault(x => x.UserName == username);
            if (user == null)
            {
                return true;
            }
            return false;
        }

        public string GenerateToken(AppUser user, IEnumerable<string> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtOptions.SecretKey);

            List<Claim> claimList = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email,user.Email!),
                new Claim(JwtRegisteredClaimNames.Name,user.Name!),
                new Claim(JwtRegisteredClaimNames.Sub,user.Id!),
            };

            claimList.AddRange(roles.Select(x => new Claim(ClaimTypes.Role, x))); // x meaning role

            var tokenDescripter = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claimList),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _jwtOptions.Audience,
                Issuer = _jwtOptions.Issuer
            };

            var token = tokenHandler.CreateToken(tokenDescripter);

            return tokenHandler.WriteToken(token);
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO model)
        {
            AppUser? user = await _dbContext.AppUsers.FirstOrDefaultAsync(x => x.UserName!.ToLower() == model.UserName.ToLower());

            bool isValid = await _userManager.CheckPasswordAsync(user!, model.Password);

            if (user == null || isValid == false)
            {
                return new LoginResponseDTO()
                {
                    appUserDto = null,
                    Token = null
                };
            }
            // there user is valid and exists in db .. so we will generate token.

            var roles = await _userManager.GetRolesAsync(user);

            var token = this.GenerateToken(user, roles); // responsible for generating token

            AppUserDto appUserDto = _mapper.Map<AppUserDto>(user);

            return new LoginResponseDTO()
            {
                appUserDto = appUserDto,
                Token = token
            };
        }
    }
}
