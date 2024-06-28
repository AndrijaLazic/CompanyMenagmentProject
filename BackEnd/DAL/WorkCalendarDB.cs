using DOMAIN.Exceptions.SQL;
using DOMAIN.Models.Database;
using DOMAIN.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class WorkCalendarDB
    {
        private CompanyMenagmentProjectContext _databaseContext;

        public WorkCalendarDB(CompanyMenagmentProjectContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public void AddRegistrations(WorkCalendarDTO[] calendarDTO)
        {
            for (int i = 0; i < calendarDTO.Length; i++) {
                WorkCalendar workCalendar = new WorkCalendar()
                {
                    Date = calendarDTO[i].Date,
                    Shift = calendarDTO[i].Shift,
                    UserId = calendarDTO[i].UserId,
                };
                _databaseContext.WorkCalendars.Add(workCalendar);
            }
            _databaseContext.SaveChanges();
        }


        public void RemoveWorkRegistrations(WorkCalendarDTO[] calendarDTO) {
            for (int i = 0; i < calendarDTO.Length; i++)
            {
                WorkCalendar workCalendar = _databaseContext.WorkCalendars.Where(x=>x.UserId == calendarDTO[i].UserId && x.Date == calendarDTO[i].Date && x.Shift == calendarDTO[i].Shift).FirstOrDefault();
                if(workCalendar == null)
                {
                    throw new BaseSqlException("WorkRegistrationNotFound");
                }
                _databaseContext.WorkCalendars.Remove(workCalendar);
            }
            _databaseContext.SaveChanges();
        }
    }
}
