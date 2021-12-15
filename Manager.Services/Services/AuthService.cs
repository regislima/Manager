using AutoMapper;
using Manager.Domain.entities;
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
            return password.Equals(hash);
        }
    }
}