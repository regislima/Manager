using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Manager.Core.Exceptions;
using Manager.Core.Extensions;
using Manager.Domain.entities;
using Manager.Infra.Interfaces;
using Manager.Security.Criptography;
using Manager.Services.DTO;
using Manager.Services.Interfaces;

namespace Manager.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<UserDTO> Create(UserDTO objDTO)
        {
            var userExists = await _userRepository.FindByEmail(objDTO.Email);

            if (!userExists.IsNull())
                throw new DomainException("Já existe cadastro com email informado");
            
            objDTO.CreatedAt = DateTime.Now;
            objDTO.UpdatedAt = null;
            User user = _mapper.Map<User>(objDTO);
            user.ChangePassword(Convert.ToBase64String(new CryptService().SHA256Hash(objDTO.Password)));
            user.Validate();
            User userCreated = await _userRepository.Create(user);

            return _mapper.Map<UserDTO>(userCreated);
        }

        public async Task<List<UserDTO>> FindAll()
        {
            var users = await _userRepository.FindAll();

            return _mapper.Map<List<UserDTO>>(users);
        }

        public async Task<UserDTO> FindByEmail(string email)
        {
            var userExists = await _userRepository.FindByEmail(email);

            return _mapper.Map<UserDTO>(userExists);
        }

        public async Task<UserDTO> FindById(long id)
        {
            var userExists = await _userRepository.FindById(id);

            return _mapper.Map<UserDTO>(userExists);
        }

        public async Task<List<UserDTO>> FindByName(string name)
        {
            var users = await _userRepository.FindByName(name);

            return _mapper.Map<List<UserDTO>>(users);
        }

        public async Task Remove(long id)
        {
            await _userRepository.Remove(id);
        }

        public async Task<UserDTO> Update(UserDTO objDTO)
        {
            User userExists = await _userRepository.FindById(objDTO.Id);

            if (userExists.IsNull())
                throw new DomainException("Usuário não encontrado");
            
            // Verifica se existe algum usuário cadastrado com o email informado
            var emailExists = await _userRepository.FindByEmail(objDTO.Email);

            if (!emailExists.IsNull())
                throw new DomainException("Já existe cadastro com email informado");
            
            objDTO.CreatedAt = userExists.CreatedAt;
            objDTO.UpdatedAt = DateTime.Now;
            userExists = _mapper.Map<User>(objDTO);

            byte[] hashedPassword = Convert.FromBase64String(userExists.Password);
            CryptService cryptService = new CryptService();

            // Verifica se houve mudança na senha
            if (!cryptService.CheckHash(objDTO.Password, hashedPassword))
                userExists.ChangePassword(Convert.ToBase64String(new CryptService().SHA256Hash(objDTO.Password)));
            
            userExists.Validate();
            User userUpdated = await _userRepository.Update(userExists);

            return _mapper.Map<UserDTO>(userUpdated);
        }
    }
}