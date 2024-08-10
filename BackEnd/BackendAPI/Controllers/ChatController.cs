using BLL.Services;
using DOMAIN.Models.Database;
using DOMAIN.Models.DTR;
using DOMAIN.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace BackendAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private SharedDB _sharedDB;
        private readonly CommunicationService _communicationService;
        private readonly AppConfigClass _appConfiguration;
        private readonly ILogger<ChatController> _logger;

        public ChatController(CommunicationService communicationService,SharedDB sharedDB, IOptions<AppConfigClass> appConfiguration, ILogger<ChatController> logger)
        {
            _sharedDB = sharedDB;
            _appConfiguration = appConfiguration.Value;
            _logger = logger;
            _communicationService = communicationService;
        }

        [HttpGet("GetMessages/{id}")]
        public async Task<ActionResult<CommunicationMessageDTR[]>> GetMessages(int id)
        {
            ServiceResponse<CommunicationMessageDTR[]> serviceResponse = new ServiceResponse<CommunicationMessageDTR[]>();
            var accessToken = Request.Headers[HeaderNames.Authorization];
            string tokenValue = accessToken[0]!.Split(" ")[1];

            serviceResponse.Data = _communicationService.GetMessagesFromCommunication(id, tokenValue);
            return Ok(serviceResponse);
        }

        [HttpGet("GetMyChats")]
        public async Task<ActionResult<UserCommunicationDTR[]>> GetMyChats(int id)
        {
            ServiceResponse<UserCommunicationDTR[]> serviceResponse = new ServiceResponse<UserCommunicationDTR[]>();
            var accessToken = Request.Headers[HeaderNames.Authorization];
            string tokenValue = accessToken[0]!.Split(" ")[1];

            serviceResponse.Data = _communicationService.GetUserCommunications(tokenValue);
            return Ok(serviceResponse);
        }
    }
}
