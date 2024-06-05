using System.Text.Json.Serialization;

namespace Ernesto.Sanchez.OrderService.Infrastructure.Security.Extensions
{
    public class AccessRoles
    {
        [JsonPropertyName("roles")]
        public List<string> Roles { get; set; }
    }
}
