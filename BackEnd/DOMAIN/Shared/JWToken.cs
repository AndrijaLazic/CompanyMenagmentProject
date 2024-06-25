using DOMAIN.Models.Database;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace DOMAIN.Shared
{
    public static class JWToken
    {
        public static string CreateToken(User user,JWTSettings jWTSettings)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("phoneNumber",user.PhoneNumber),
                new Claim("email",user.Email),
                new Claim("name",user.Name),
                new Claim("id",user.Id.ToString()),
                new Claim("lastname",user.Lastname),
                new Claim("workerType",user.WorkerType.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jWTSettings.SECRET_KEY));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(double.Parse(jWTSettings.JWT_DURRATION)),
                signingCredentials: creds,
                issuer: jWTSettings.ISSUER
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        public static string? ValidateToken(string token, JWTSettings jWTSettings)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jWTSettings.SECRET_KEY);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,

                    ClockSkew = TimeSpan.Zero
                },
                out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                var Id = jwtToken.Claims.First(x => x.Type == "id").Value;

                return Id;
            }
            catch
            {
                return null;
            }

        }
    }
}
