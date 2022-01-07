using System;
using AutoMapper;
using Manager.Domain.Entities;
using Manager.Security.Criptography;
using Manager.Services.DTO;
using Microsoft.Extensions.Configuration;

namespace Manager.Services.Services
{
    public class AuthService
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration, IMapper mapper)
        {
            _mapper = mapper;
            _configuration = configuration;
        }

        public string GenerateJWTToken(UserDTO userDTO)
        {
            User user = _mapper.Map<User>(userDTO);
            return TokenService.GenerateJWTToken(_configuration, user);
        }

        public bool CheckPassword(string password, string hash)
        {
            byte[] hashArray = Convert.FromBase64String(hash);
            
            return new CryptService().CheckHash(password, hashArray);
        }
    }
}