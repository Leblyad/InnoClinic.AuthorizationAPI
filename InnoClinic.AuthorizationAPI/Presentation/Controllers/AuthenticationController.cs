using InnoClinic.AuthorizationAPI.Application.Services.Abstractions;
using InnoClinic.AuthorizationAPI.Core.Entities.Models.AuthorizationDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.AuthorizationAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAccountService _authenticationService;

        public AuthenticationController(IAccountService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="userForCreation"></param>
        /// 
        [Authorize]
        [HttpGet("q")]
        public async Task<IActionResult> RegisterUser()
        {
            //var user = await _authenticationService.CreateUser(userForCreation, role);
            return Ok("ХУЕТА");
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserForCreationDto userForCreation)
        {
            var user = await _authenticationService.CreateUser(userForCreation, "Patient");
            return Created(nameof(RegisterUser), user);
        }

        /// <summary>
        /// Add role to user
        /// | Required role: Administrator
        /// </summary>
        /// <param name="login"></param>
        /// <param name="role"></param>
        [HttpPost]
        [Route("add_role")]
        public async Task<IActionResult> AddRoleToUser([FromQuery] string login, [FromQuery] string role)
        {
            await _authenticationService.AddRoleToUser(login, role);
            return Ok();
        }

        /// <summary>
        /// Authenticate user by username and password
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Returns access token for authenticated user</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
        {
            var userInfo = await _authenticationService.AuthenticateUser(user);
            return Ok(userInfo);
        }

        [HttpPost("signout")]
        public async Task<IActionResult> SignOut()
        {
            await _authenticationService.SignOutUser();
            return Ok();
        }
    }
}
