using Microsoft.AspNetCore.Identity;

namespace AuthorizationAPI.Core.Entities.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
