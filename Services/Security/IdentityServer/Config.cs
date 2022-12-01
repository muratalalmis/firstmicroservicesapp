using System.Collections.Generic;
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

        };

        public static List<TestUser> TestUsers => new List<TestUser>()
        {

        };
    }
}

