using InnoClinic.AuthorizationAPI.Core.Entities.Models;
using InnoClinic.AuthorizationAPI.Infrastructure.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.AuthorizationAPI.Infrastructure.Repository
{
    public class AuthenticationDbContext : IdentityDbContext<User>
    {
        public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureTables(modelBuilder);
        }

        private void ConfigureTables(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RoleConfiguration());

            modelBuilder.Entity<User>(entity => entity.ToTable(name: "Users"));
            modelBuilder.Entity<IdentityRole>(entity => entity.ToTable(name: "Roles"));
            modelBuilder.Entity<IdentityUserRole<string>>(entity => entity.ToTable(name: "UserRoles"));
            modelBuilder.Entity<IdentityUserClaim<string>>(entity => entity.ToTable(name: "UserClaims"));
            modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.ToTable(name: "UserLogins"));
            modelBuilder.Entity<IdentityUserToken<string>>(entity => entity.ToTable(name: "UserTokens"));
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity => entity.ToTable(name: "RoleClaims"));
        }
    }
}
