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
                ClientId = "catalogAPIClient",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                AllowedScopes = { "catalogAPI" }
            },
            new Client
            {
                ClientId = "basketAPIClient",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                AllowedScopes = { "basketAPI" }
            },
            new Client
            {
                ClientId = "orderingAPIClient",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                AllowedScopes = { "orderingAPI" }
            },
        };

        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
        {
            new ApiScope("catalogAPI", "Catalog API"),
            new ApiScope("basketAPI", "Basket API"),
            new ApiScope("orderingAPI", "Ordering API"),
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
                Password = "123456",
                Claims = new List<Claim>()
                {
                    new Claim(JwtClaimTypes.GivenName, "murat"),
                    new Claim(JwtClaimTypes.FamilyName, "alalmis")
                }
            }
        };
    }
}

