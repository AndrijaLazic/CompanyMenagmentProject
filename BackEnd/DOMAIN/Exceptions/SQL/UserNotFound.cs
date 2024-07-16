using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Exceptions.SQL
{
    [Serializable]
    public class UserNotFound : BaseSqlException
    {
        public UserNotFound() { }
        public UserNotFound(string message)
            : base(message)
        {

        }
    }
}
