using Ernesto.Sanchez.OrderService.Infrastructure.Http.Extensions;
using Ernesto.Sanchez.OrderService.Infrastructure.Persistence.Extension;
using Ernesto.Sanchez.OrderService.Infrastructure.Persistence.Helpers.DataMapping.TypeHelper;
using Ernesto.Sanchez.OrderService.Infrastructure.Security.Extensions;

namespace Ernesto.Sanchez.OrderService.Infrastructure.Extensions
{
    public class InfrastructureOptions
    {
        public string ConnectionString { get; set; } 
 
    }
    public static class InfrastructureExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, Action<InfrastructureOptions> configure)
        {
            var options = new InfrastructureOptions();
            configure(options);

            services.AddHttp();
            services.AddPersistence(opt=>opt.ConnectionString = options.ConnectionString);
            services.AddSecurity();
            
        }
    }
}
