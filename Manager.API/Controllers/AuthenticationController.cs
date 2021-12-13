using System;
using System.Threading.Tasks;
using AutoMapper;
using Manager.API.ViewModels.Auth;
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
        public async Task<ActionResult> authenticate([FromBody] AuthView authView)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ViewResponse.Error(authView, ModelState.GetErrorMessage()));

                UserDTO userDTO = _mapper.Map<UserDTO>(authView);
                userDTO = await _userService.FindByEmail(userDTO.Email);

                if (userDTO.IsNull())
                    return BadRequest(ViewResponse.Error(authView, "Email ou senha inválidos."));

                AuthDTO authDTO = new AuthDTO();
                authDTO.UserDTO = userDTO;
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