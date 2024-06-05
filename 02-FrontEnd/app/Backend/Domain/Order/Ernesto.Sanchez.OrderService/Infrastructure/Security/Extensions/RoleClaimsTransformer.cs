using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ernesto.Sanchez.OrderService.Infrastructure.Security.Extensions
{
    public class RoleClaimsTransformer : IClaimsTransformation
    {
        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var claimsIdentity = (ClaimsIdentity)principal.Identity;
                        
            var realmAccessClaim = claimsIdentity.FindFirst("realm_access");
            var resourceAccessClaim = claimsIdentity.FindFirst("resource_access");

            if (realmAccessClaim != null)
            {
                var realmAccess = JsonSerializer.Deserialize<AccessRoles>(realmAccessClaim.Value);
                foreach (var role in realmAccess.Roles)
                {
                    claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role));
                }
            }

            if (resourceAccessClaim != null)
            {                
                var resourceAccess = JsonSerializer.Deserialize<Dictionary<string, AccessRoles>>(resourceAccessClaim.Value);
                foreach (var accessRoles in resourceAccess.Values)
                {
                    foreach (var role in accessRoles.Roles)
                    {
                        claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role));
                    }
                }
            }

            return Task.FromResult(principal);
        }
    }
}
