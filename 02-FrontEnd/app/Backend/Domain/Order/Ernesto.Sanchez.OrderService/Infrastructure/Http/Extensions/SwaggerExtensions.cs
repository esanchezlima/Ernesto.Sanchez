using Microsoft.OpenApi.Models;

namespace Ernesto.Sanchez.OrderService.Infrastructure.Http.Extensions
{
    public static class SwaggerExtensions
    {
        public static void AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("TokenAuthNZ",
                new()
                {
                    Name = "Authorization",
                    Description = "Token-based authentication and authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    In = ParameterLocation.Header
                });
                options.AddSecurityRequirement(new()
                {
                    {
                        new ()
                        {
                            Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                            Id = "TokenAuthNZ" }
                        }, new List<string>()}
                });
            });
        }
    }
}
