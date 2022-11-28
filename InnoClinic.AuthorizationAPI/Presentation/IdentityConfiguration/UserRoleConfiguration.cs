using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InnoClinic.AuthorizationAPI.Presentation.IdentityConfiguration
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            AddInitialData(builder);
        }

        private void AddInitialData(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData
            (
                new IdentityUserRole<string>
                {
                    RoleId = "0fc5b095-096f-4a3a-be41-fad238b2a81a",
                    UserId = "31b9b272-53d0-4f6f-a190-0d5c70e242b4"
                }
            );
        }
    }
}
