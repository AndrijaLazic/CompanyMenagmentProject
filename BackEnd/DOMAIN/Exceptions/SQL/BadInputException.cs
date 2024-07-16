using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Exceptions.SQL
{
    [Serializable]
    public class BadInputException : BaseSqlException
    {
        public BadInputException() { }
        public BadInputException(string message)
            : base(message)
        {

        }
    }
}
