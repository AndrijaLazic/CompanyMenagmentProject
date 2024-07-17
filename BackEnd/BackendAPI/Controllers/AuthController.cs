using DOMAIN.Abstractions;
using DOMAIN.Models.DTO;
using DOMAIN.Models.DTR;
using DOMAIN.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Serilog;

namespace BackendAPI.Controllers
{


    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppConfigClass _appConfiguration;
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IOptions<AppConfigClass> appConfiguration, IAuthService userService, ILogger<AuthController> logger)
        {
            _appConfiguration = appConfiguration.Value;
            _authService = userService;
            _logger = logger;
        }

        [HttpPost("Register")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<bool>> Register(RegistrationDTO dto)
        {
            ServiceResponse<bool> response = new ServiceResponse<bool>();
            
            response = await _authService.RegisterUser(dto);
            
            return Ok(response);
        }

        [HttpPost("Login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<string>> Login(LoginDTO dto)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
          
            response = await _authService.Login(dto);
            
            return Ok(response);
        }


    }
}
