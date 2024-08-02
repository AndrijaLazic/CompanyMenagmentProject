using DOMAIN.Exceptions.SQL;
using DOMAIN.Models.Database;
using DOMAIN.Models.DTO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DOMAIN.Models.DTR;

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


        public List<WorkCalendar> GetWorkCalendarForUser(int userId, DateOnly date, int offset, int numOfRows)
        {

            SqlParameter[] parms =
            [
                // Create parameter(s)    
                new SqlParameter { ParameterName = "@Id", Value = userId },
                new SqlParameter { ParameterName = "@Date", Value = date.ToString("yyyy/MM/dd") },
                new SqlParameter { ParameterName = "@Offset", Value =  offset},
                new SqlParameter { ParameterName = "@NumOfRows", Value = numOfRows }
            ];

            string sql = $"EXEC spGetUsersShifts @Id, @Date, @Offset, @NumOfRows";

            List<WorkCalendar> workCalendars = null;

            var res = _databaseContext.WorkCalendars.FromSqlRaw(sql, parms);

            workCalendars = res.ToList();

            
            return workCalendars;
        }

        public List<WorkCalendarAllUsersDTR> GetWorkCalendarForAllUsers(DateOnly date, int offset, int numOfRows)
        {

            SqlParameter[] parms =
            [
                new SqlParameter { ParameterName = "@Date", Value = date.ToString("yyyy/MM/dd") },
                new SqlParameter { ParameterName = "@Offset", Value =  offset},
                new SqlParameter { ParameterName = "@NumOfRows", Value = numOfRows }
            ];

            string sql = $"EXEC spGetShifsForAllUsers @Date, @Offset, @NumOfRows";

            var res = _databaseContext.Database.SqlQueryRaw<WorkCalendarAllUsersDTR>(sql, parms);

            return res.ToList();
        }

        public ShiftType[] GetShiftTypes()
        {
            return _databaseContext.ShiftTypes.ToArray();
        }

        private void ExecuteWithDynamic(string sql, SqlParameter[] parms)
        {
            using (var cmd = _databaseContext.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = sql;
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                cmd.Parameters.AddRange(parms);


                var retObject = new List<dynamic>();
                using (var dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var dataRow = new ExpandoObject() as IDictionary<string, object>;
                        for (var fieldCount = 0; fieldCount < dataReader.FieldCount; fieldCount++)
                            dataRow.Add(dataReader.GetName(fieldCount), dataReader[fieldCount]);

                        retObject.Add((ExpandoObject)dataRow);
                    }
                }
                var lista = retObject.ToList();
            }
        }
    }
}
