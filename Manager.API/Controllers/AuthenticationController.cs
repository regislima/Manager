using System;
using System.Threading.Tasks;
using AutoMapper;
using Manager.API.ViewModels;
using Manager.Core.Communication;
using Manager.Core.Exceptions;
using Manager.Core.Extensions;
using Manager.Services.DTO;
using Manager.Services.Interfaces;
using Manager.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace Manager.API.Controllers
{
    [ApiController]
    [Route("api")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly AuthService _authService;
        private readonly IMapper _mapper;

        public AuthenticationController(IUserService userService, AuthService authService, IMapper mapper)
        {
            _userService = userService;
            _authService = authService;
            _mapper = mapper;
        }
        
        [HttpPost]
        [Route("v1/auth/login")]
        public async Task<ActionResult> authenticate([FromBody] AuthInputView authInputView)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ViewResponse.Error(authInputView, ModelState.GetErrorMessage()));

                UserDTO userDTO = await _userService.FindByEmail(authInputView.Email);

                if (userDTO.IsNull())
                    return BadRequest(ViewResponse.Error(authInputView, "Email ou senha inválidos."));

                AuthDTO authDTO = _mapper.Map<AuthDTO>(userDTO);
                
                if (!_authService.CheckPassword(authInputView.Password, userDTO.Password))
                    return BadRequest(ViewResponse.Error(authInputView, "Email ou senha inválidos."));
                
                authDTO.Token = _authService.GenerateJWTToken(userDTO);

                return Ok(ViewResponse.Success(authDTO));
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
    }
}