using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Exceptions.SQL
{
    [Serializable]
    public class DataBaseDuplicateException : BaseSqlException
    {
        public DataBaseDuplicateException() { }
        public DataBaseDuplicateException(string message)
            : base(message)
        {

        }
    }
}
