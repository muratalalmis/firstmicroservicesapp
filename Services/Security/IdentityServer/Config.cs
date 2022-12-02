using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace IdentityServer
{
    public class Config
    {
        public static IEnumerable<Client> Clients => new Client[]
        {
            new Client
            {
                ClientId = "catalogClient",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                AllowedScopes = { "catalogAPI" }
            },
            new Client()
            {
                ClientId = "catalog_app_client",
                ClientName = "Catalog App Client",
                AllowedGrantTypes = GrantTypes.Code,
                AllowRememberConsent = false,
                RedirectUris = new List<string>()
                {
                    // TODO:
                    "https://localhost:7000"
                },
                PostLogoutRedirectUris = new List<string>()
                {
                    // TODO: 
                    "https:localhost:7000/sign-out-callback-oidc"
                },
                ClientSecrets = new List<Secret>()
                {
                    new Secret("secret".Sha256())
                },
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                }
            }
        };

        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
        {
            new ApiScope("catalogAPI", "Catalog API")
        };

        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {

        };

        public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };

        public static List<TestUser> TestUsers => new List<TestUser>()
        {
            new TestUser()
            {
                SubjectId = "EC163A84-B0FE-44FB-8F92-04A93100800A",
                Username = "murat",
                Password = "murat",
                Claims = new List<Claim>()
                {
                    new Claim(JwtClaimTypes.GivenName, "murat"),
                    new Claim(JwtClaimTypes.FamilyName, "alalmis")
                }
            }
        };
    }
}

