using InnoClinic.AuthorizationAPI.Application.Services.Abstractions;
using InnoClinic.AuthorizationAPI.Application.Services;
using InnoClinic.AuthorizationAPI.Core.Entities.Contracts;
using InnoClinic.AuthorizationAPI.Core.Entities.Models;
using InnoClinic.AuthorizationAPI.Infrastructure;
using InnoClinic.AuthorizationAPI.Infrastructure.Configuration;
using InnoClinic.AuthorizationAPI.Infrastructure.Repository;
using InnoClinic.AuthorizationAPI.Presentation.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using InnoClinic.AuthorizationAPI.Presentation.Middlewares;

namespace InnoClinic.AuthorizationAPI
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
                .AddInMemoryClients(IdentityConfiguration.BuildClients())
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

        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
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
                        b.MigrationsAssembly("InnoClinic.AuthorizationAPI")));
        }
    }
}
