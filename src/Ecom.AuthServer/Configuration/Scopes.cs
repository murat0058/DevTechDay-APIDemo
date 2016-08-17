using IdentityServer4.Models;
using System.Collections.Generic;

namespace Ecom.AuthServer.Configuration
{
    public class Scopes
    {
        public static IEnumerable<Scope> Get()
        {
            return new List<Scope>()
            {
                StandardScopes.OpenId,
                StandardScopes.Profile,
                StandardScopes.OfflineAccess,
                StandardScopes.Roles,
                StandardScopes.Phone,
                new Scope //Custom Scope
                {
                    Name = "idmgr",
                    DisplayName = "Thinktecture IdentityManager",
                    Type = ScopeType.Resource,
                    Emphasize = true,
                    IncludeAllClaimsForUser = true,
                    Required = true
                }
            };
        }
    }
}
