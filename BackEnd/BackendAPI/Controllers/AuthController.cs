using DOMAIN.Abstractions;
using DOMAIN.Models.DTO;
using DOMAIN.Models.DTR;
using DOMAIN.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BackendAPI.Controllers
{


    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppConfigClass _appConfiguration;
        private readonly IAuthService _userService;

        public AuthController(IOptions<AppConfigClass> appConfiguration, IAuthService userService)
        {
            _appConfiguration = appConfiguration.Value;
            _userService = userService;
        }

        [HttpPost("Register")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<bool>> Register(RegistrationDTO dto)
        {
            ServiceResponse<bool> response = new ServiceResponse<bool>();
            try
            {
                response = await _userService.RegisterUser(dto);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return BadRequest(response);
            }
            return Ok(response);
        }

        
    }
}
