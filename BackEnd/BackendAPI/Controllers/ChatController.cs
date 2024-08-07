using BLL.Services;
using DOMAIN.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BackendAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private SharedDB _sharedDB;
        private readonly AppConfigClass _appConfiguration;
        private readonly ILogger<ChatController> _logger;

        public ChatController(SharedDB sharedDB, IOptions<AppConfigClass> appConfiguration, ILogger<ChatController> logger)
        {
            _sharedDB = sharedDB;
            _appConfiguration = appConfiguration.Value;
            _logger = logger;
        }
    }
}
