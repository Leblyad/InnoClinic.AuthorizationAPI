using AuthorizationAPI.Application.Serrvices.Abstractions;
using AuthorizationAPI.Application.Services;
using AuthorizationAPI.Core.Entities.Contracts;
using AuthorizationAPI.Core.Entities.Models;
using AuthorizationAPI.Infrastructure;
using AuthorizationAPI.Infrastructure.Configuration;
using AuthorizationAPI.Infrastructure.Repository;
using AuthorizationAPI.Presentation.Configuration;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;

namespace AuthorizationAPI
{
    public static class ServiceExtensions
    {
        public static void ConfigureSwagger(this IServiceCollection services)
            => services.AddSwaggerGen(setup =>
            {
                setup.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Place to add JWT with Bearer",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                setup.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        { Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Name = "Bearer",
                        },
                        new List<string>()
                    }
                });
            });

        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<User, IdentityRole>(config =>
            {
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireDigit = false;
                config.Password.RequireUppercase = false;

            }).AddEntityFrameworkStores<AuthenticationDbContext>()
            .AddDefaultTokenProviders();
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddIdentityServer()
                .AddAspNetIdentity<User>()
                .AddInMemoryApiResources(IdentityConfiguration.BuildApiResources())
                .AddInMemoryClients(IdentityConfiguration.BuildClients(configuration))
                .AddInMemoryIdentityResources(IdentityConfiguration.BuildIdentityResources())
                .AddInMemoryApiScopes(IdentityConfiguration.BuildApiScopes())
                .AddProfileService<ProfileService>()
                .AddDeveloperSigningCredential();

            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "Identity.Cookie";
                config.LoginPath = "/Account/Login";
            });
        }

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationManager, AuthenticationManager>();
            services.AddScoped<IAccountService, AccountService>();
        }

        public static void ConfigureDbConnection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AuthenticationDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("sqlConnection"), b =>
                        b.MigrationsAssembly("AuthorizationAPI")));
        }
    }
}
