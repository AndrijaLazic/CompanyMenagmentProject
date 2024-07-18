using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Shared
{
    public class Database
    {
        public string ConnectionString { get; set; }
    }

    public class JWTSettings
    {
        public string SECRET_KEY { get; set; }
        public string JWT_DURRATION { get; set; }
        public string ISSUER {  get; set; }
    }
    public class AppConfigClass
    {
        public string AllowedHosts { get; set; }
        public Database Database { get; set; }
        public JWTSettings JWTSettings { get; set; }
    }
}
