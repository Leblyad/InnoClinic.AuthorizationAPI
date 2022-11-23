using InnoClinic.AuthorizationAPI.Application.Services.Abstractions;
using InnoClinic.AuthorizationAPI.Core.Entities.Models.AuthorizationDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.AuthorizationAPI.Presentation.Controllers
{
    [Route("api/authentication")]
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
        /// <response code="200">Returns user info (username, email, role).</response>
        /// <response code="404">Role not found.</response>
        /// <response code="500">Operation wasn't succeeded.</response>
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserForCreationDto userForCreation)
        {
            var user = await _authenticationService.CreateUserAsync(userForCreation);
            return Created(nameof(RegisterUser), user);
        }

        /// <summary>
        /// Add role to user
        /// </summary>
        /// <param name="email"></param>
        /// <param name="role"></param>
        /// <response code="200">User role changed.</response>
        /// <response code="404">Role or user not found.</response>
        [HttpPost]
        [Route("role")]
        public async Task<IActionResult> ChangeUserRole([FromQuery] string email, [FromQuery] string role)
        {
            await _authenticationService.ChangeUserRoleAsync(email, role);
            return Ok();
        }

        /// <summary>
        /// Authenticate user by username and password
        /// </summary>
        /// <param name="user"></param>
        /// <response code="200">Returns user info (access token, refresh token, role).</response>
        /// <response code="401">Unauthorized.</response>
        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
        {
            var userInfo = await _authenticationService.AuthenticateUserAsync(user);
            return Ok(userInfo);
        }

        /// <summary>
        /// Signout user
        /// </summary>
        /// <response code="200">User signout.</response>
        [HttpPost("signout")]
        public async Task<IActionResult> SignOut()
        {
            await _authenticationService.SignOutUserAsync();
            return Ok();
        }
    }
}
