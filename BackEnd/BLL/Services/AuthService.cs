using Azure;
using DAL;
using DOMAIN.Abstractions;
using DOMAIN.Exceptions.Server;
using DOMAIN.Models.Database;
using DOMAIN.Models.DTO;
using DOMAIN.Models.DTR;
using DOMAIN.Shared;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppConfigClass _options;
        private IUserDataDB _userDataDB;

        public AuthService(IOptions<AppConfigClass> options, IUserDataDB userDataDB)
        {
            _options = options.Value;
            _userDataDB = userDataDB;
        }

        public async Task<ServiceResponse<bool>> RegisterUser(RegistrationDTO registrationDTO)
        {
            ServiceResponse<bool> response = new ServiceResponse<bool>();

            User user = new User()
            {
                Name = registrationDTO.Name,
                Lastname = registrationDTO.Lastname,
                Email = registrationDTO.Email,
                WorkerType = registrationDTO.WorkerType,
                PhoneNumber = registrationDTO.PhoneNumber,
            };

            PasswordLogic.CreatePasswordHash(registrationDTO.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;
            _userDataDB.InsertNewUser(user);

                

            response.Data = true;


            return response;
        }

        public async Task<ServiceResponse<TokensDTR>> Login(LoginDTO loginDTO)
        {
            
            PasswordLogic.CreatePasswordHash(loginDTO.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User user = _userDataDB.GetUser(loginDTO.Email);

            if (!PasswordLogic.VerifyPasswordHash(loginDTO.Password, user.PasswordHash, user.PasswordSalt))
            {
                throw new Exception("BadPassword");
            }

            ServiceResponse<TokensDTR> response = new ServiceResponse<TokensDTR>();
            response.Data = new TokensDTR();
            response.Data.JWT = JWToken.CreateToken(user, _options.JWTSettings);
            response.Data.ResetToken = JWToken.CreateRefreshToken(user, _options.ResetTokenSettings);

            return response;
        }

        public void RemoveUser(int id)
        {
            _userDataDB.RemoveUser(id);
        }

        public string ResetJWT(string resetToken)
        {
            string? id = JWToken.ValidateRefreshToken(resetToken, _options.ResetTokenSettings);
            if (id == null)
            {
                throw new BaseServerException("ResetTokenNotValid");
            }

            User user = _userDataDB.GetUser(int.Parse(id));

            return JWToken.CreateToken(user, _options.JWTSettings);

        }
    }
}
