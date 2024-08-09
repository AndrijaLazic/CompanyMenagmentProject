using BLL.Services;
using DOMAIN.Models.Database;
using DOMAIN.Models.DTO;
using DOMAIN.Models.DTR;
using DOMAIN.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    //[Authorize]
    public class UserController : ControllerBase
    {
        private readonly AppConfigClass _appConfiguration;
        private readonly CommunicationService _communicationService;
        private readonly ILogger<UserController> _logger;

        public UserController(IOptions<AppConfigClass> appConfiguration, CommunicationService userService, ILogger<UserController> logger)
        {
            _appConfiguration = appConfiguration.Value;
            _communicationService = userService;
            _logger = logger;
        }

        
    }
}
