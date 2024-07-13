using DOMAIN.Abstractions;
using DOMAIN.Models.Database;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UserDataDB: IUserDataDB
    {
        private CompanyMenagmentProjectContext _databaseContext;

        public UserDataDB(CompanyMenagmentProjectContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public int InsertNewUser(User user)
        {
            SqlParameter[] parms = 
            [
                // Create parameter(s)    
                new SqlParameter { ParameterName = "@Name", Value = user.Name },
                new SqlParameter { ParameterName = "@Lastname", Value = user.Lastname },
                new SqlParameter { ParameterName = "@Email", Value = user.Email },
                new SqlParameter { ParameterName = "@PasswordHash", Value = user.PasswordHash },
                new SqlParameter { ParameterName = "@PasswordSalt", Value = user.PasswordSalt },
                new SqlParameter { ParameterName = "@PhoneNumber", Value = user.PhoneNumber },
                new SqlParameter { ParameterName = "@WorkerType", Value = user.WorkerType },
            ];

            string sql = $"EXEC spInsertNewUser @Name, @Lastname ,@Email ,@PasswordHash ,@PasswordSalt,@PhoneNumber,@WorkerType";
            int res = 0;
            try
            {
                res = _databaseContext.Database.ExecuteSqlRaw(sql, parms);
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    //constraint error
                    case 2627:
                        switch (ex.Message)
                        {
                            case String message when (message.Contains("UQ_PhoneNumber")):
                                throw new Exception("PhoneNumberTaken");
                            case String message when (message.Contains("UQ_Email")):
                                throw new Exception("EmailAlreadyTaken");
                            default:
                                throw;
                        }  
                    default:
                        throw;
                }
            }

            if (res == 0)
                throw new Exception("InsertNewUserError");

            return res;
        }


    }
}
