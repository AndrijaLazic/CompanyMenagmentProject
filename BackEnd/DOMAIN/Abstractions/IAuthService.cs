﻿using DOMAIN.Models.DTO;
using DOMAIN.Models.DTR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Abstractions
{
    public interface IAuthService
    {
        Task<ServiceResponse<bool>> RegisterUser(RegistrationDTO registrationDTO);
        Task<ServiceResponse<TokensDTR>> Login(LoginDTO loginDTO);
        void RemoveUser(int id);
        string ResetJWT(string resetToken);

    }
}
