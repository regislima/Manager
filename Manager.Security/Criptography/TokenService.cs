using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Manager.Domain.entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Manager.Security.Criptography
{
    public class TokenService
    {
        public static string GenerateJWTToken(IConfiguration configuration, User user)
        {
            int tokenExpiredTimeLapse = int.Parse(configuration["TokenExpireTimeLapse"]);
            byte[] key = Encoding.UTF8.GetBytes(configuration["SecretKey"]);
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),

                Issuer = configuration["Issuer"],
                Audience = configuration["Audience"],
                Expires = DateTime.UtcNow.AddHours(tokenExpiredTimeLapse),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            SecurityToken token = jwtSecurityTokenHandler.CreateToken(tokenDescriptor);

            return jwtSecurityTokenHandler.WriteToken(token);
        }
    }
}