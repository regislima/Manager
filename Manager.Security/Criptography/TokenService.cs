using System;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Manager.Core.Extensions;
using Manager.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Extensions;

namespace Manager.Security.Criptography
{
    public class TokenService
    {
        public static string GenerateJWTToken(IConfiguration configuration, User user)
        {
            int tokenExpiredTimeLapse = int.Parse(configuration["Jwt:TokenExpireTimeLapse"]);
            byte[] key = Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]);
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, user.Role.GetEnumDescription())
                }),

                Issuer = configuration["Jwt:Issuer"],
                Audience = configuration["Jwt:Audience"],
                Expires = DateTime.UtcNow.AddHours(tokenExpiredTimeLapse),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            SecurityToken token = jwtSecurityTokenHandler.CreateToken(tokenDescriptor);

            return jwtSecurityTokenHandler.WriteToken(token);
        }
    }
}