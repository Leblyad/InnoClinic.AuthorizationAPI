using InnoClinic.AuthorizationAPI.Core.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InnoClinic.AuthorizationAPI.Presentation.IdentityConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            AddInitialData(builder);
        }

        private void AddInitialData(EntityTypeBuilder<User> builder)
        {
            var user = new User
            {
                Id = "31b9b272-53d0-4f6f-a190-0d5c70e242b4",
                UserName = "UserName",
                NormalizedUserName = "USERNAME",
                FirstName = "UserName",
                LastName = "UserName",
                Email = "UserName@UserName.com",
                NormalizedEmail = "USERNAME@USERNAME.COM",
                PhoneNumber = "XXXXXXXXXXXXX",
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
                SecurityStamp = new Guid().ToString("D")
            };
            user.PasswordHash = PassGenerate(user);
            builder.HasData(user);
        }

        private string PassGenerate(User user)
        {
            var passHash = new PasswordHasher<User>();
            return passHash.HashPassword(user, "password");
        }
    }
}
