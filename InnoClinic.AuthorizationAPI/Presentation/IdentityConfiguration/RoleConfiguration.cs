using InnoClinic.AuthorizationAPI.Core.Entities.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InnoClinic.AuthorizationAPI.Infrastructure.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            AddInitialData(builder);
        }

        private void AddInitialData(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData
            (
                new IdentityRole
                {
                    Name = nameof(UserRole.Receptionist),
                    NormalizedName = nameof(UserRole.Receptionist).ToUpper()
                },
                new IdentityRole
                {
                    Name = nameof(UserRole.Doctor),
                    NormalizedName = nameof(UserRole.Doctor).ToUpper()
                },
                new IdentityRole
                {
                    Name = nameof(UserRole.Patient),
                    NormalizedName = nameof(UserRole.Patient).ToUpper()
                }
            );
        }
    }
}
