using BLL.Services;
using DOMAIN.Abstractions;
using DOMAIN.Models.DTO;
using DOMAIN.Models.DTR;
using DOMAIN.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BackendAPI.Controllers
{
    [Route("[controller]")]
    
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AppConfigClass _appConfiguration;
        private readonly AdminService _adminService;
        private readonly ILogger<AdminController> _logger;

        public AdminController(IOptions<AppConfigClass> appConfiguration, AdminService adminService, ILogger<AdminController> logger)
        {
            _appConfiguration = appConfiguration.Value;
            _adminService = adminService;
            _logger = logger;
        }


        [HttpPost("AddWorkRegistration")]
        public async Task<ActionResult<string>> AddWorkRegistration(WorkCalendarDTO[] calendarDTO)
        {
            ServiceResponse<string> serviceResponse = new ServiceResponse<string>();
            _adminService.AddWorkRegistration(calendarDTO);
            serviceResponse.Message = "RegisteredWorkSuccessfully";
            return Ok(serviceResponse);
        }

        [HttpDelete("RemoveWorkRegistration")]
        public async Task<ActionResult<string>> RemoveWorkRegistration(WorkCalendarDTO[] calendarDTO)
        {
            ServiceResponse<string> serviceResponse = new ServiceResponse<string>();
            _adminService.RemoveWorkRegistration(calendarDTO);
            serviceResponse.Message = "RegisteredWorkRemovedSuccessfully";
            return Ok(serviceResponse);
        }

    }
}
