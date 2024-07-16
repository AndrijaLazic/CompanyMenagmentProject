using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Exceptions.SQL
{
    [Serializable]
    public class BaseSqlException : Exception
    {
        public BaseSqlException() { }
        public BaseSqlException(string message)
            : base(message)
        {

        }
    }
}
