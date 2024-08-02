using DOMAIN.Abstractions;
using DOMAIN.Models.DTR;
using DOMAIN.Shared;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService
    {
        private readonly AppConfigClass _options;
        private IUserDataDB _userDataDB;

        public UserService(IOptions<AppConfigClass> options, IUserDataDB userDataDB)
        {
            _options = options.Value;
            _userDataDB = userDataDB;
        }
    }
}
