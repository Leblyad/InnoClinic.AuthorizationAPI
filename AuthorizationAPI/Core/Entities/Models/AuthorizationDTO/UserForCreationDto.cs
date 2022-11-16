﻿using System.ComponentModel.DataAnnotations;

namespace AuthorizationAPI.Core.Entities.Models.AuthorizationDTO
{
    public class UserForCreationDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required(ErrorMessage = "UserName - required field")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password - required field")]
        public string Password { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}
