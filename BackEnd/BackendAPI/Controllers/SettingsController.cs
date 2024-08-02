using BLL.Services;
using DOMAIN.Models.DTR;
using DOMAIN.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BackendAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly AppConfigClass _appConfiguration;
        private readonly GlobalDataService _globalDataService;
        private readonly ILogger<SettingsController> _logger;

        public SettingsController(IOptions<AppConfigClass> appConfiguration, GlobalDataService globalDataService, ILogger<SettingsController> logger)
        {
            _appConfiguration = appConfiguration.Value;
            _globalDataService = globalDataService;
            _logger = logger;
        }

        [HttpGet("GetFrontEndSettings")]
        public ActionResult<List<UserDTR>> GetFrontEndSettings()
        {
            ServiceResponse<FrontEndSettingsDTR> serviceResponse = new ServiceResponse<FrontEndSettingsDTR>();
            serviceResponse.Data = _globalDataService.GetFrontEndSettingsDTR();
            return Ok(serviceResponse);
        }
    }
}
