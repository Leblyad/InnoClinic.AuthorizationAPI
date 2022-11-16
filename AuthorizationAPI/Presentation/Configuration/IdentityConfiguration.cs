using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace AuthorizationAPI.Infrastructure.Configuration
{
    public class IdentityConfiguration
    {
        public static string ScopeAPI => "APIClient";
        public static string ScopeRoles => "roles";

        public static IEnumerable<Client> BuildClients(IConfiguration configuration)
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "APIClient",
                    RequireClientSecret = false,

                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        ScopeAPI,
                    },

                    AllowOfflineAccess = true,
                }
            };
        }

        public static IEnumerable<ApiResource> BuildApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource(ScopeAPI, new []{JwtClaimTypes.Name,  JwtClaimTypes.Role})
                {
                    Scopes =
                    {
                        ScopeAPI,
                        ScopeRoles,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        IdentityServerConstants.StandardScopes.Profile,
                    },

                }
              };
        }

        public static IEnumerable<IdentityResource> BuildIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource {Name = ScopeRoles, UserClaims={JwtClaimTypes.Role} },
            };
        }

        public static IEnumerable<ApiScope> BuildApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope(ScopeAPI)
            };
        }
    }
}
