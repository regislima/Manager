using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Manager.API.ViewModels;
using Manager.Core.Communication;
using Manager.Core.Exceptions;
using Manager.Core.Extensions;
using Manager.Services.DTO;
using Manager.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Manager.API.Controller
{
    [ApiController]
    [Route("api")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("v1/users/create")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Create([FromBody] UserInputView userInputView)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ViewResponse.Error(userInputView, ModelState.GetErrorMessage()));
                
                UserDTO userDTO = _mapper.Map<UserDTO>(userInputView);
                UserDTO userCreated = await _userService.Create(userDTO);
                
                return Ok(ViewResponse.Success(userCreated));
            }
            catch (DomainException ex)
            {
                return BadRequest(ViewResponse.Error(ex.Errors, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ViewResponse.Error(null, ex.Message));
            }
        }

        [HttpPut]
        [Route("v1/users/update")]
        [Authorize(Roles = "Admiministrator, Normal")]
        public async Task<IActionResult> Update([FromBody] UserInputView userInputView)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ViewResponse.Error(userInputView, ModelState.GetErrorMessage()));
                
                UserDTO userDTO = _mapper.Map<UserDTO>(userInputView);
                UserDTO userUpdated = await _userService.Update(userDTO);
                
                return Ok(ViewResponse.Success(userUpdated));
            }
            catch (DomainException ex)
            {
                return BadRequest(ViewResponse.Error(ex.Errors, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ViewResponse.Error(null, ex.Message));
            }
        }

        [HttpDelete]
        [Route("v1/users/remove/{id}")]
        [Authorize(Roles = "Admiministrator")]
        public async Task<IActionResult> Remove(long id)
        {
            try
            {
                await _userService.Remove(id);
                
                return Ok(ViewResponse.Success(null));
            }
            catch (DomainException ex)
            {
                return BadRequest(ViewResponse.Error(ex.Errors));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ViewResponse.Error(null, ex.Message));
            }
        }

        [HttpGet]
        [Route("v1/users/find/id")]
        [Authorize(Roles = "Admiministrator, Normal")]
        public async Task<IActionResult> Find([FromQuery] long id)
        {
            try
            {
                UserDTO user = await _userService.FindById(id);

                return (user.IsNull() ? Ok(ViewResponse.Success(null, "Nenhum registro encontrado")) : Ok(ViewResponse.Success(user)));
            }
            catch (DomainException ex)
            {
                return BadRequest(ViewResponse.Error(ex.Errors));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ViewResponse.Error(null, ex.Message));
            }
        }

        [HttpGet]
        [Route("v1/users/find/all")]
        [Authorize(Roles = "Admiministrator")]
        public async Task<IActionResult> FindAll()
        {
            try
            {
                List<UserDTO> usersList = await _userService.FindAll();

                return (usersList.IsNull() ? Ok(ViewResponse.Success(null, "Nenhum registro encontrado")) : Ok(ViewResponse.Success(usersList)));
            }
            catch (DomainException ex)
            {
                return BadRequest(ViewResponse.Error(ex.Errors));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ViewResponse.Error(null, ex.Message));
            }
        }

        [HttpGet]
        [Route("v1/users/find/email")]
        [Authorize(Roles = "Admiministrator")]
        public async Task<IActionResult> FindByEmail([FromQuery] string email)
        {
            try
            {
                UserDTO users = await _userService.FindByEmail(email);

                return (users.IsNull() ? Ok(ViewResponse.Success(null, "Nenhum registro encontrado")) : Ok(ViewResponse.Success(users)));
            }
            catch (DomainException ex)
            {
                return BadRequest(ViewResponse.Error(ex.Errors));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ViewResponse.Error(null, ex.Message));
            }
        }

        [HttpGet]
        [Route("v1/users/find/name")]
        [Authorize(Roles = "Admiministrator")]
        public async Task<IActionResult> FindByName([FromQuery] string name)
        {
            try
            {
                List<UserDTO> usersList = await _userService.FindByName(name);

                return (usersList.Count == 0 ? Ok(ViewResponse.Success(null, "Nenhum registro encontrado")) : Ok(ViewResponse.Success(usersList)));
            }
            catch (DomainException ex)
            {
                return BadRequest(ViewResponse.Error(ex.Errors));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ViewResponse.Error(null, ex.Message));
            }
        }
    }
}