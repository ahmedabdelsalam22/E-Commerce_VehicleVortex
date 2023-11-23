using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using VehicleVortex.Models;
using VehicleVortex.Models.Dto;
using VehicleVortex.Services.AuthServices.IAuthServices;

namespace VehicleVortex.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LoginResponseDTO>> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            LoginResponseDTO loginResponse = await _service.Login(loginRequestDTO);
            if (loginResponse.appUserDto == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                return BadRequest(loginResponse);
            }
            return Ok(loginResponse);
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AppUserDto>> Register([FromBody] RegisterRequestDTO model)
        {
            if (model.Name.ToLower() == model.UserName.ToLower())
            {
                ModelState.AddModelError("", "username and name are the same!");
            }
            bool ifUserNameUnique = _service.IsUniqueUser(model.UserName);
            if (!ifUserNameUnique)
            {
                ModelState.AddModelError("", "Username already exists");
            }

            var userDTO = await _service.Register(model);

            if (userDTO != null)
            {
                return userDTO;
            }
            else
            {
                return new AppUserDto();
            }
        }


    }
}
