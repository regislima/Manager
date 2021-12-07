using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Manager.Core.Exceptions;
using Manager.Core.Extensions;
using Manager.Domain.entities;
using Manager.Infra.Interfaces;
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
                throw new DomainException("Usuário já cadastrado com email informado");
            
            User user = _mapper.Map<User>(objDTO);
            user.Validate();

            user.CreatedAt = DateTime.Now;
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
            var userExists = await _userRepository.FindById(objDTO.Id);

            if (userExists.IsNull())
                throw new DomainException("Usuário não encontrado");
            
            User user = _mapper.Map<User>(objDTO);
            user.Validate();

            user.UpdatedAt = DateTime.Now;
            User userUpdated = await _userRepository.Update(user);

            return _mapper.Map<UserDTO>(userUpdated);
        }
    }
}