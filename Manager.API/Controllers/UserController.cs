using System;
using System.Threading.Tasks;
using AutoMapper;
using Manager.API.ViewModels.User;
using Manager.Core.Communication;
using Manager.Core.Exceptions;
using Manager.Core.Extensions;
using Manager.Services.DTO;
using Manager.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Manager.API.Controller
{
    [ApiController]
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
        [Route("/api/v1/users/create")]
        public async Task<IActionResult> Create([FromBody] UserViewCreate userView)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ViewResponse.Error(ModelState.GetErrorMessage()));
                
                UserDTO userDTO = _mapper.Map<UserDTO>(userView);
                UserDTO userCreated = await _userService.Create(userDTO);
                
                return Ok(ViewResponse.Success(userCreated));    
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