using DOMAIN.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Abstractions
{
    public interface IUserDataDB
    {
        public int InsertNewUser(User user);
        public User GetUser(string mail);
        public void RemoveUser(int id);
    }
}
