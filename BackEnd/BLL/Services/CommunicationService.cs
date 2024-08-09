using DAL;
using DOMAIN.Abstractions;
using DOMAIN.Exceptions.SQL;
using DOMAIN.Models.Database;
using DOMAIN.Models.DTR;
using DOMAIN.Shared;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CommunicationService
    {
        private readonly AppConfigClass _options;
        private CommunicationDB _communicationDB;

        public CommunicationService(IOptions<AppConfigClass> options, CommunicationDB communicationDB)
        {
            _options = options.Value;
            _communicationDB = communicationDB;
        }

        public CommunicationMessage[] GetMessagesFromCommunication(int communicationID, string tokenValue)
        {
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(tokenValue);
            int userId = int.Parse(jwt.Claims.First(x => x.Type == "id").Value);

            UserCommunication? communication = _communicationDB.GetCommunication(communicationID);
            if(communication == null)
            {
                throw new BadInputException($"Communication not found{communicationID}");
            }
            if(communication.User1 != userId && communication.User2 != userId)
            {
                throw new BadInputException($"UserId:{userId} is not in communication {communicationID}");
            }

            return _communicationDB.GetAllMessages(communicationID);
        }

        public UserCommunication[] GetUserCommunications(string token)
        {
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
            int userId = int.Parse(jwt.Claims.First(x => x.Type == "id").Value);
            return _communicationDB.GetUserCommunications(userId);
        }


    }
}
