using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data;
using Api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AcountController : ControllerBase
    {
        private readonly UserManager<ApiUser> _useManager;
        private readonly ILogger<AcountController> _logger;
        private readonly IMapper _mapper;

        public AcountController(UserManager<ApiUser> useManager,
                        ILogger<AcountController> logger,
                        IMapper mapper)
        {
            _useManager = useManager;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpPost]
        [Route("Register")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
        {
            _logger.LogInformation($"Registration Attempt for {userDTO.Email}");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var user = _mapper.Map<ApiUser>(userDTO);
                user.UserName = userDTO.Email;
                var result = await _useManager.CreateAsync(user,userDTO.Password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest(ModelState);
                }
                await _useManager.AddToRolesAsync(user, userDTO.Roles);
                return Accepted();
            }
            catch (Exception ex)
            {

                _logger.LogError(ex,$"Something Went Wrong in the {nameof(Register)}");
                return Problem($"Something Went Wrong in the {nameof(Register)}",statusCode:500);
            }
        }
        ////[HttpPost]
        ////[Route("Login")]
        ////public async Task<IActionResult> Login([FromBody] LoginUserDTO userDTO)
        ////{
        ////    _logger.LogInformation($"Registration Attempt for {userDTO.Email}");
        ////    if (!ModelState.IsValid)
        ////    {
        ////        return BadRequest(ModelState);
        ////    }
        ////    try
        ////    {
        ////        var result = await _signInManager.PasswordSignInAsync(userDTO.Email,userDTO.Password,false,false);
        ////        if (!result.Succeeded) 
        ////        {
        ////            return Unauthorized(userDTO);
        ////        }
        ////        return Accepted();
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        _logger.LogError(ex, $"Something Went Wrong in the {nameof(Login)}");
        ////        return Problem($"Something Went Wrong in the {nameof(Login)}", statusCode: 500);
        ////    }
        ////}
    }
}