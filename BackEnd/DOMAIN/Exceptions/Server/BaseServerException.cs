using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Exceptions.Server
{
    [Serializable]
    public class BaseServerException : Exception
    {
        public BaseServerException() { }
        public BaseServerException(string message)
            : base(message)
        {

        }
    }
}

