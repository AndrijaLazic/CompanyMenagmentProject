using DAL;
using DOMAIN.Abstractions;
using DOMAIN.Models.Database;
using DOMAIN.Models.DTO;
using DOMAIN.Models.DTR;
using DOMAIN.Shared;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class WorkerService
    {
        private readonly AppConfigClass _options;
        private WorkCalendarDB _workCalendarDB;

        public WorkerService(IOptions<AppConfigClass> options, WorkCalendarDB workCalendarDB)
        {
            _options = options.Value;
            _workCalendarDB = workCalendarDB;
        }

        public async Task<List<WorkCalendar>> GetWorkCalendarForUser(string userToken, DateOnly date , int offset, int numOfRows)
        {
            string userId = JWToken.ValidateToken(userToken,_options.JWTSettings);
            if(userId == null)
            {
                throw new Exception("BadJWToken");
            }

            return _workCalendarDB.GetWorkCalendarForUser(int.Parse(userId), date, offset ,numOfRows);
        }

        public async Task<List<WorkCalendarAllUsersDTR>> GetWorkCalendarForAllUsers(DateOnly date, int offset, int numOfRows)
        {

            return _workCalendarDB.GetWorkCalendarForAllUsers(date, offset, numOfRows);
        }
    }
}
