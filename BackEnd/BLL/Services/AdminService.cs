using DAL;
using DOMAIN.Abstractions;
using DOMAIN.Models.DTO;
using DOMAIN.Shared;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AdminService
    {
        private readonly AppConfigClass _options;
        private WorkCalendarDB _workCalendarDB;

        public AdminService(IOptions<AppConfigClass> options, IUserDataDB userDataDB, WorkCalendarDB workCalendarDB)
        {
            _options = options.Value;
            _workCalendarDB = workCalendarDB;
        }

        public void AddWorkRegistration(WorkCalendarDTO[] calendarDTO)
        {
            _workCalendarDB.AddRegistrations(calendarDTO);
        }

        public void RemoveWorkRegistration(WorkCalendarDTO[] calendarDTO)
        {
            _workCalendarDB.RemoveWorkRegistrations(calendarDTO);
        }
    }
}
