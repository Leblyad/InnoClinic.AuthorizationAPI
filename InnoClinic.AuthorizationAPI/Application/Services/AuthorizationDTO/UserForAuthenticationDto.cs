using System.ComponentModel.DataAnnotations;

namespace InnoClinic.AuthorizationAPI.Core.Entities.Models.AuthorizationDTO
{
    public class UserForAuthenticationDto
    {
        [Required(ErrorMessage = "Email - required field")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password - required field")]
        public string Password { get; set; }
    }
}
