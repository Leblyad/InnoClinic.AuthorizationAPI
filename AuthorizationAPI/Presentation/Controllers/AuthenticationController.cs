using AuthorizationAPI.Application.Serrvices.Abstractions;
using AuthorizationAPI.Core.Entities.Models.AuthorizationDTO;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationAPI.Presentation.Controllers
{
    [Route("api/Authentication")]
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
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserForCreationDto userForCreation)
        {
            await _authenticationService.CreateUser(userForCreation);
            return Ok();
        }

        /// <summary>
        /// Add role to user
        /// | Required role: Administrator
        /// </summary>
        /// <param name="login"></param>
        /// <param name="role"></param>
        [HttpPost]
        [Route("AddRole")]
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
