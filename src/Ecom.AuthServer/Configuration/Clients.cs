using IdentityServer4.Models;
using System.Collections.Generic;

namespace Ecom.AuthServer.Configuration
{
    public class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new List<Client>()
            {
                new Client()
                {
                    ClientId = "ecomApp",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("1a1f67ad-1608-4614-82a0-9257ebc31a56".Sha256())
                    },
                    ClientName = "My Ecom App",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    RefreshTokenUsage = TokenUsage.OneTimeOnly,

                    AllowedScopes = new List<string>
                    {
                        StandardScopes.OpenId.Name,
                        StandardScopes.Profile.Name,
                        StandardScopes.OfflineAccess.Name
                    }
                }
            };
        }
    }
}
