﻿using BLL.Services;
using DOMAIN.Models.Database;
using DOMAIN.Models.DTO;
using DOMAIN.Models.DTR;
using DOMAIN.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace BackendAPI.Controllers
{
    
    [Route("[controller]")]
    [Authorize]
    [ApiController]
    public class WorkCalendarController : ControllerBase
    {
        private readonly AppConfigClass _appConfiguration;
        private readonly WorkerService _workerService;
        private readonly ILogger<AdminController> _logger;

        public WorkCalendarController(IOptions<AppConfigClass> appConfiguration, WorkerService workerService, ILogger<AdminController> logger)
        {
            _appConfiguration = appConfiguration.Value;
            _workerService = workerService;
            _logger = logger;
        }

        [HttpPost("GetWorkCalendar")]
        public async Task<ActionResult<List<WorkCalendar>>> AddWorkRegistration(GetWorkCalendarDTO dto)
        {
            DateOnly date = DateOnly.Parse(dto.dateString);
            ServiceResponse<List<WorkCalendar>> serviceResponse = new ServiceResponse<List<WorkCalendar>>();
            var accessToken = Request.Headers[HeaderNames.Authorization];
            string tokenValue = accessToken[0]!.Split(" ")[1];


            serviceResponse.Data = await _workerService.GetWorkCalendarForUser(tokenValue, date);

            return Ok(serviceResponse);
        }
    }
}
