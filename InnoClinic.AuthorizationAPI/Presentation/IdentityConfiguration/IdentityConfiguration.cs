using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace InnoClinic.AuthorizationAPI.Infrastructure.Configuration
{
    public class IdentityConfiguration
    {
        public static string ScopeAPI => "APIClient";

        public static IEnumerable<Client> BuildClients()
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
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.OfflineAccess
                    },

                    AllowOfflineAccess = true
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
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        IdentityServerConstants.StandardScopes.Profile,
                        ScopeAPI
                    },

                }
              };
        }

        public static IEnumerable<IdentityResource> BuildIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
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
